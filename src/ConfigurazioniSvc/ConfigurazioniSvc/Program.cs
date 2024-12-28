using ConfigurazioniSvc.BusinessLayer.Mapper;
using ConfigurazioniSvc.BusinessLayer.Validation;
using ConfigurazioniSvc.DataAccessLayer;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.RedisCache.Options;
using GSWCloudApp.Common.Routing;
using Serilog;

namespace ConfigurazioniSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var policyCorsName = "AllowAll";
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog((context, config) =>
        {
            var assemblyProject = typeof(Program).Assembly.GetName().Name!.ToString();
            var romeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
            var utcNow = DateTimeOffset.UtcNow;
            var romeTime = TimeZoneInfo.ConvertTime(utcNow, romeTimeZone);

            config.ReadFrom.Configuration(context.Configuration);
            config.Enrich.WithProperty("Application", assemblyProject);
            config.Enrich.WithProperty("Timestamp", romeTime);
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        var postgresConnection = builder.Configuration.GetConnectionString("SqlConfigurazioni")
            ?? throw new InvalidOperationException("Connection database string not valid.");

        var redisOptions = builder.Services.ConfigureAndGet<RedisOptions>(builder.Configuration, nameof(RedisOptions))
            ?? throw new InvalidOperationException("Redis options not found.");

        var appOptions = builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration, nameof(ApplicationOptions))
            ?? throw new InvalidOperationException("Application options not found.");

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(postgresConnection, appOptions);
        builder.Services.ConfigureCors(policyCorsName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureSwagger();

        builder.Services.AddAntiforgery();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureRedisCache(redisOptions);

        builder.Services.ConfigureServices<MappingProfile, CreateConfigurazioneValidator>();
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

        versionedApi.MapEndpoints();
        await app.RunAsync();
    }
}