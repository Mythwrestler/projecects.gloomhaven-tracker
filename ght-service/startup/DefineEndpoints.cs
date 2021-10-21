using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace GloomhavenTracker.Service.Startup;

public static class Define
{
    public static void Endpoints (WebApplication app)
    {
        var helloEnd = app.MapGet("hello-world", () => "Hello World").RequireAuthorization("authenticated");
        if(app.Environment.IsDevelopment()) helloEnd.AllowAnonymous();
    }
}