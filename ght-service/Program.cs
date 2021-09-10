using Microsoft.OpenApi.Models;
using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ght_service", Version = "v1" });
});
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ght_service v1"));
}

app.UseCors((config) => {
    config.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("hello-world", () => "Hello World");

app.MapGet("hello-name", (HttpContext context) => $"Hello {context.Request.Query["name"]}");

app.MapHub<ChatHub>("chat");

await app.RunAsync();
