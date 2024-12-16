using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewaySvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configFiles = Directory.GetFiles("ConfigOcelot", "*.json");

        foreach (var configFile in configFiles)
        {
            builder.Configuration.AddJsonFile(configFile, optional: false, reloadOnChange: true);
        }

        //builder.Configuration.AddJsonFile("ConfigOcelot/ocelot.configurazioni.json", optional: false, reloadOnChange: true);
        //builder.Configuration.AddJsonFile("ConfigOcelot/ocelot.categorie.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot();

        builder.Services.Configure<KestrelServerOptions>(builder.Configuration.GetSection("Kestrel"));
        builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        var app = builder.Build();

        app.UseOcelot().Wait();
        app.Run();
    }
}