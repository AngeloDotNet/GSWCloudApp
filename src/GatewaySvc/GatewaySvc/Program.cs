using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewaySvc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var postgresConnection = builder.Configuration.GetConnectionString("SqlAutentica")
            ?? throw new InvalidOperationException("Connection database string not valid.");

        var appOptions = builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration, nameof(ApplicationOptions))
            ?? throw new InvalidOperationException(nameof(ApplicationOptions));

        var jwtOptions = builder.Services.ConfigureAndGet<JwtOptions>(builder.Configuration, nameof(JwtOptions))
            ?? throw new InvalidOperationException(nameof(JwtOptions));

        var securityOptions = new SecurityOptions();
        builder.Services.ConfigureDbContextAsync<Program, SecurityDbContext>(postgresConnection, appOptions);

        var configFiles = Directory.GetFiles("ConfigOcelot", "*.json");

        foreach (var configFile in configFiles)
        {
            builder.Configuration.AddJsonFile(configFile, optional: false, reloadOnChange: true);
        }

        builder.Services.AddOcelot();
        builder.Services.ConfigureAuthFullTokenJWT<SecurityDbContext>(securityOptions, jwtOptions);

        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseOcelot().Wait();
        app.Run();
    }
}