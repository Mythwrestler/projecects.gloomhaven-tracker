using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GloomhavenTracker.Service.BackgroundServices;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Repos;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();
// Add services to the container.


bool authEnabled = bool.Parse(Environment.GetEnvironmentVariable("AUTH_ENABLED") ?? "false");
string authAuthority = Environment.GetEnvironmentVariable("AUTH_AUTHORITY") ?? String.Empty;
string authAudience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE") ?? String.Empty;
bool httpLoggingEnabled = bool.Parse(Environment.GetEnvironmentVariable("HTTP_LOGGING_ENABLED") ?? "false");

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

builder.Services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "My API",
                Description = "My First ASP.NET Core Web API",
                TermsOfService = new System.Uri("https://www.talkingdotnet.com"),
                Contact = new OpenApiContact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com" }
            });
 
            c.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "New API V2",
                Description = "Sample Web API",
                TermsOfService = new System.Uri("https://www.talkingdotnet.com"),
                Contact = new OpenApiContact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com" }
            });
        });

builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

builder.Services.AddSingleton<ICombatRepo, CombatRepo>();
builder.Services.AddSingleton<ICombatService, CombatService>();
builder.Services.AddHostedService<BattleHubMonitor>();

if(httpLoggingEnabled)
{
    builder.Services.AddHttpLogging(options => {
        options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
        options.RequestBodyLogLimit = 4096;
        options.RequestBodyLogLimit = 4096;
    });
}

var app = builder.Build();

var logger = app.Logger;

if(httpLoggingEnabled) {
    app.UseHttpLogging();

        // app.Use(async (context, next) =>
        // {
        //     logger.LogTrace(context.Request.Body.)
        //     // Do work that doesn't write to the Response.
        //     await next.Invoke();
        //     // Do logging or other work that doesn't write to the Response.
        // });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
    });
        
}

app.UseCors((config) =>
{
    config
        .WithMethods("POST", "GET")
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:5025", "http://localhost.fiddler:5025");
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

var battleHub = app.MapHub<CombatHub>("hub/combatspace");
if (authEnabled)
{
    battleHub.RequireAuthorization("authenticated", "superuser");
}
else
{
    battleHub.AllowAnonymous();
}

app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

app.Run();