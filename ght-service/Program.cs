using GloomhavenTracker.Service.Hubs;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Gloom Haven Tracker", Version = "v1" });
});

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

app.MapGet("hello-world", () => "Hello World");

app.MapGet("hello-name", (HttpContext context) => $"Hello {context.Request.Query["name"]}");

app.MapHub<BattleHub>("battle");

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gloom Haven Tracker v1"));

await app.RunAsync();
