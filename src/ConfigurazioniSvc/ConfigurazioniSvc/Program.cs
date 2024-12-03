using System.Text.Json.Serialization;
using ConfigurazioniSvc.BusinessLayer.Mapper;
using ConfigurazioniSvc.BusinessLayer.Validation;
using ConfigurazioniSvc.DataAccessLayer;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.Routing;

namespace ConfigurazioniSvc;

/// <summary>
/// The main entry point for the application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method for the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static async Task Main(string[] args)
    {
        var policyCorsName = "AllowAll";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        var assemblyProject = typeof(Program).Assembly.GetName().Name!.ToString().ToLower();
        var postgresConnection = await ApplicationExtensions.GetVaultStringConnectionAsync(builder, assemblyProject, "connection");
        var redisConnection = await ApplicationExtensions.GetVaultStringConnectionAsync(builder, "redis", "connection");

        var applicationOptions = builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration,
            nameof(ApplicationOptions)) ?? throw new InvalidOperationException("Application options not found.");

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(postgresConnection, applicationOptions);
        builder.Services.ConfigureCors(policyCorsName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureSwagger();

        builder.Services.AddAntiforgery();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureRedisCache(builder.Configuration, redisConnection);

        builder.Services.ConfigureServices<MappingProfile, CreateConfigurazioneValidator>();
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        app.ApplyMigrations<AppDbContext>();
        app.UseExceptionHandler();

        app.UseStatusCodePages();
        app.UseDevSwagger(applicationOptions);

        app.UseForwardNetworking();
        app.UseRouting();

        app.UseCors(policyCorsName);
        app.UseAntiforgery();

        //app.UseAuthorization();
        versionedApi.MapEndpoints();

        app.Run();
    }
}
