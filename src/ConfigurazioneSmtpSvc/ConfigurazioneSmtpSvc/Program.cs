using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Handlers.GetAll;
using ConfigurazioneSmtpSvc.BusinessLayer.Services;
using ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;
using ConfigurazioneSmtpSvc.BusinessLayer.Validator;
using ConfigurazioneSmtpSvc.DataAccessLayer;
using GSWCloudApp.Common;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Routing;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioneSmtpSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var applicationOptions = await MicroservicesExtensions.GetApplicationOptionsAsync(builder.Configuration);
        var databaseConnection = await MicroservicesExtensions.GetConnectionStringFromNamingAsync(builder.Configuration, "SqlConfigSmtp");

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(applicationOptions, databaseConnection);
        builder.Services.AddDefaultServices(BLConstants.DefaultCorsPolicyName);

        builder.Services.AddAntiforgery();
        builder.Services.AddTransient<IConfigurazioneService, ConfigurazioneService>();

        builder.Services.ConfigureMediatR<GetAllSettingSmtpHandler>();
        builder.Services.ConfigureGenericServices();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureFluentValidation<CreateSettingSenderValidator>();

        builder.Services.AddOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        await app.ApplyMigrationsAsync<AppDbContext>();

        app.UseDefaultServices(applicationOptions, BLConstants.DefaultCorsPolicyName);
        app.UseAntiforgery();

        versionedApi.MapEndpoints();
        await app.RunAsync();
    }
}