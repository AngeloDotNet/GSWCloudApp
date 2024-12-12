using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AutenticazioneSvc.LoadBalancer;

public static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add reverse proxy services and load configuration from appsettings
        builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("LoadBalancer"));

        // Configure Kestrel server options
        builder.Services.Configure<KestrelServerOptions>(builder.Configuration.GetSection("Kestrel"));

        // Configure route options to use lowercase URLs
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        var app = builder.Build();

        // Map reverse proxy routes
        app.MapReverseProxy();

        // Run the application
        app.Run();
    }
}