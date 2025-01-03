using ConfigurazioniSvc.BusinessLayer.Mapper;
using ConfigurazioniSvc.BusinessLayer.Validation;
using ConfigurazioniSvc.DataAccessLayer;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Routing;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioniSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        var postgresConnection = IConfigurationExtensions.GetDatabaseConnection(builder, "SqlConfigurazioni");
        var appOptions = IConfigurationExtensions.GetApplicationOptions(builder);

        var redisOptions = IConfigurationExtensions.GetRedisOptions(builder);

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(postgresConnection, appOptions);
        builder.Services.ConfigureCors(BLConstants.DefaultCorsPolicyName);

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

        app.UseCors(BLConstants.DefaultCorsPolicyName);
        app.UseAntiforgery();

        versionedApi.MapEndpoints();
        await app.RunAsync();
    }
}