using System.Text.Json;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddJsonConsole();
// Add services to the container.


bool authEnabled = bool.Parse(Environment.GetEnvironmentVariable("AUTH_ENABLED") ?? "false");
string authAuthority = Environment.GetEnvironmentVariable("AUTH_AUTHORITY") ?? String.Empty;
string authAudience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE") ?? String.Empty;

var authBuilder = builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

if (authEnabled)
{
    authBuilder.AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/battle")))
                {
                        // Read the token out of the query string
                        context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
        options.Authority = authAuthority;
        options.Audience = authAudience;
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("authenticated", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        options.AddPolicy("superuser", new AuthorizationPolicyBuilder().RequireRole("superuser").Build());
    });

}

builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddCors();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

builder.Services.AddScoped<IBattleRepo, BattleRepo>();
builder.Services.AddScoped<IBattleService, BattleService>();

builder.Services.AddHttpLogging(options => {
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors((config) =>
{
    config
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:5025");
});

app.UseAuthentication();
app.UseAuthorization();

var helloEndPoint = app.MapGet("hello-world", () => "Hello World");
if (authEnabled)
{
    helloEndPoint.RequireAuthorization("superuser");
}
else
{
    helloEndPoint.AllowAnonymous();
}

var battleHub = app.MapHub<BattleHub>("battle");
if (authEnabled)
{
    battleHub.RequireAuthorization("authenticated", "superuser");
}
else
{
    battleHub.AllowAnonymous();
}

app.Run();