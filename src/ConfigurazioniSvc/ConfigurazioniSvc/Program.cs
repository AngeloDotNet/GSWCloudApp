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

        builder.Services.AddDefaultServices(BLConstants.DefaultCorsPolicyName);
        builder.Services.AddTransient<IConfigurazioniService, ConfigurazioniService>();

        builder.Services.ConfigureMediatR<GetConfigurationsHandler>();
        builder.Services.ConfigureProblemDetails();

        builder.Services.AddOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        app.UseDefaultServices(applicationOptions, BLConstants.DefaultCorsPolicyName);
        app.UseAntiforgery();

        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}