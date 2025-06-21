using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Yarp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        var app = builder.Build();

        app.MapReverseProxy();

        app.MapGet("/", () => Results.Extensions.Html("For API1 Swagger click <a href=\"/api1/swagger\">here</a><br />For API2 Swagger click <a href=\"/api2/swagger\">here</a>"));

        app.Run();
    }
}