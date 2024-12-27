using AutenticazioneSvc.BusinessLayer.HostedService;
using AutenticazioneSvc.BusinessLayer.Services;
using AutenticazioneSvc.DataAccessLayer;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.Routing;
using Serilog;

namespace AutenticazioneSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var policyCorsName = "AllowAll";
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, config) =>
        {
            var assemblyProject = typeof(Program).Assembly.GetName().Name!;
            var romeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
            var utcNow = DateTimeOffset.UtcNow;
            var romeTime = TimeZoneInfo.ConvertTime(utcNow, romeTimeZone);

            config.ReadFrom.Configuration(context.Configuration);
            config.Enrich.WithProperty("Application", assemblyProject);
            config.Enrich.WithProperty("Timestamp", romeTime);
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        var postgresConnection = builder.Configuration.GetConnectionString("SqlAutentica")
            ?? throw new ArgumentNullException("Connection database string not valid.");

        var appOptions = builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration, nameof(ApplicationOptions))
            ?? throw new ArgumentNullException(nameof(ApplicationOptions));

        var jwtOptions = builder.Services.ConfigureAndGet<JwtOptions>(builder.Configuration, nameof(JwtOptions))
            ?? throw new ArgumentNullException(nameof(JwtOptions));

        var securityOptions = new SecurityOptions();

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(postgresConnection, appOptions);
        builder.Services.ConfigureCors(policyCorsName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureAuthSwagger();

        builder.Services.ConfigureAuthFullTokenJWT<AppDbContext>(securityOptions, jwtOptions);
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddAntiforgery();
        builder.Services.AddHostedService<AuthStartupTask>();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        await app.ApplyMigrationsAsync<AppDbContext>();
        app.UseExceptionHandler();

        app.UseStatusCodePages();
        app.UseDevSwagger(appOptions);

        app.UseForwardNetworking();
        app.UseRouting();

        app.UseCors(policyCorsName);
        app.UseAntiforgery();

        app.UseAuthorization();
        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}