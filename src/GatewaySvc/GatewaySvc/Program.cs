using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewaySvc;

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

        builder.Services.ConfigureJWTSettings<SecurityDbContext>(builder.Configuration);
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot().Wait();
        app.Run();
    }
}