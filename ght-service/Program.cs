using System.Text.Json;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Hubs;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Logging.AddJsonConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Gloom Haven Tracker", Version = "v1" });
});


builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

builder.Services.AddScoped<IBattleRepo, BattleRepo>();
builder.Services.AddScoped<IBattleService, BattleService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
    app.UseDeveloperExceptionPage();
    // app.UseSwagger();
    // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ght_service v1"));
}

app.UseCors((config) => {
    config
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("http://localhost:5025");
});

// app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();

app.MapControllers();

app.MapGet("battle-field", (IBattleService service) => {
    var battle = service.GetBattle();
    BattleDTO battleForReturn = new BattleDTO(){
        Initiative = battle.Initiative,
        MonsterDeck = battle.MonsterDeck,
    };
    battleForReturn.Actors.AddRange(battle.Actors);
    return battleForReturn;
});

app.MapGet("hello-world", () => "Hello World");

app.MapGet("hello-name", (HttpContext context) => $"Hello {context.Request.Query["name"]}");

app.MapHub<BattleHub>("battle");

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gloom Haven Tracker v1"));

await app.RunAsync();
