using GSWCloudApp.Common;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Routing;
using TraduzioniSvc.BusinessLayer.Mediator.Handlers.Get;
using TraduzioniSvc.BusinessLayer.Services;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace TraduzioniSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var applicationOptions = await MicroservicesExtensions.GetApplicationOptionsAsync(builder.Configuration);

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);

        builder.Services.ConfigureJsonOptions();
        builder.Services.ConfigureCors(BLConstants.DefaultCorsPolicyName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureSwagger();

        builder.Services.AddAntiforgery();
        builder.Services.AddTransient<ITranslateService, TranslateService>();

        builder.Services.AddMediator<GetTranslationsHandler>();
        builder.Services.ConfigureProblemDetails();

        builder.Services.AddOptions(builder.Configuration);

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