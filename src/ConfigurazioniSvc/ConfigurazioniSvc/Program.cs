using System.Text.Json.Serialization;
using ConfigurazioniSvc.BusinessLayer.Options;
using ConfigurazioniSvc.DependencyInjection;
using ConfigurazioniSvc.Helpers;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Routing;

namespace ConfigurazioniSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddHttpContextAccessor()
            .ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        ApplicationExtensions.GenerateServiceScope(out var serviceMemoryScopeFactory, out var servicePostgresScopeFactory);

        var postgresConnection = await ApplicationExtensions.GetSqlDatabaseConnectionAsync(builder);
        var redisConnection = await ApplicationExtensions.GetRedisConnectionAsync(builder);

        var applicationOptions = builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration, nameof(ApplicationOptions))
            ?? throw new InvalidOperationException("Application options not found in configuration.");

        builder.Services.ConfigureDbContextAsync(serviceMemoryScopeFactory, servicePostgresScopeFactory, postgresConnection, applicationOptions);
        builder.Services.ConfigureCors();

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureSwagger();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureRedisCache(builder.Configuration, redisConnection);

        builder.Services.ConfigureServices();
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();

        await ApplicationHelpers.ConfigureDatabaseAsync(app.Services);
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        if (app.Environment.IsDevelopment() || applicationOptions.SwaggerEnable)
        {
            ApplicationExtensions.UseDevSwagger(app);
        }

        app.UseRouting();
        app.UseCors("AllowAll");

        app.UseAntiforgery();

        //app.UseAuthentication();
        //app.UseAuthorization();

        versionedApi.MapEndpointsFromAssemblyContaining<Program>();
        app.Run();
    }
}