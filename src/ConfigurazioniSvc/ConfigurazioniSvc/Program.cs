using ConfigurazioniSvc.BusinessLayer.Extensions;
using ConfigurazioniSvc.BusinessLayer.Mediator.Handlers.Get;
using ConfigurazioniSvc.BusinessLayer.Services;
using GSWCloudApp.Common;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.Routing;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioniSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var applicationOptions = builder.Configuration.GetSection("ApplicationOptions").Get<ApplicationOptions>() ?? new();

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);
        ConfigurationAppExtensions.GenerateJSON(builder.Configuration, Path.Combine(Directory.GetCurrentDirectory(), BLConstants.JsonConfigurations));

        builder.Services.ConfigureJsonOptions();
        builder.Services.ConfigureCors(BLConstants.DefaultCorsPolicyName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureSwagger();

        builder.Services.AddAntiforgery();
        builder.Services.AddTransient<IConfigurazioniService, ConfigurazioniService>();

        builder.Services.ConfigureMediator<GetConfigurationsHandler>();
        builder.Services.ConfigureProblemDetails();

        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseDevSwagger(applicationOptions);
        app.UseForwardNetworking();

        app.UseRouting();
        app.UseCors(BLConstants.DefaultCorsPolicyName);

        app.UseAntiforgery();
        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}