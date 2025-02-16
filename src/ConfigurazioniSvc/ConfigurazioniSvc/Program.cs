using ConfigurazioniSvc.BusinessLayer.Extensions;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Routing;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioniSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);
        ConfigurationAppExtensions.RigenerateJSON(builder.Configuration, Path.Combine(Directory.GetCurrentDirectory(), BLConstants.JsonConfigurations));

        builder.Services.ConfigureJsonOptions();
        builder.Services.ConfigureCors(BLConstants.DefaultCorsPolicyName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureSwagger();

        builder.Services.AddAntiforgery();
        builder.Services.ConfigureProblemDetails();

        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseDevSwagger(builder.Configuration);
        app.UseForwardNetworking();

        app.UseRouting();
        app.UseCors(BLConstants.DefaultCorsPolicyName);

        app.UseAntiforgery();
        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}