using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Options;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewaySvc;

/// <summary>
/// The main entry point for the application.
/// </summary>
/// <param name="args">The command-line arguments.</param>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var securityOptions = new SecurityOptions();

        builder.Services.ConfigureDbContext<Program, SecurityDbContext>(builder.Configuration, "SqlAutentica");

        var configFiles = Directory.GetFiles("ConfigOcelot", "*.json");
        foreach (var configFile in configFiles)
        {
            builder.Configuration.AddJsonFile(configFile, optional: false, reloadOnChange: true);
        }

        builder.Services.AddOcelot();
        builder.Services.Configure<KestrelServerOptions>(builder.Configuration.GetSection("Kestrel"));

        builder.Services.ConfigureJWTSettings<SecurityDbContext>(builder.Configuration);
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot().Wait();
        app.Run();
    }
}
