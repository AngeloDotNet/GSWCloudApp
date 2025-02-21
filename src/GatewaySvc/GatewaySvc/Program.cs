using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Options;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewaySvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var databaseConnections = await MicroservicesExtensions.GetConnectionStringFromNamingAsync(builder.Configuration, "SqlAutentica");
        var applicationOptions = await MicroservicesExtensions.GetApplicationOptionsAsync(builder.Configuration);

        var jwtOptions = await MicroservicesExtensions.GetJwtOptionsAsync(builder.Configuration);
        var securityOptions = new SecurityOptions();

        builder.Services.ConfigureDbContextAsync<Program, SecurityDbContext>(applicationOptions, databaseConnections);

        var configFiles = Directory.GetFiles("ConfigOcelot", "*.json");
        foreach (var configFile in configFiles)
        {
            builder.Configuration.AddJsonFile(configFile, optional: false, reloadOnChange: true);
        }

        builder.Services.AddOcelot();
        builder.Services.Configure<KestrelServerOptions>(builder.Configuration.GetSection("Kestrel"));

        builder.Services.ConfigureJWTSettings<SecurityDbContext>(jwtOptions);
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot().Wait();
        app.Run();
    }
}