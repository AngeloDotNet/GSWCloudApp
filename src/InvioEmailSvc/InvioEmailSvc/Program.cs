using GSWCloudApp.Common;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Routing;
using InvioEmailSvc.BusinessLayer.Mediator.Handlers;
using InvioEmailSvc.BusinessLayer.Services;
using InvioEmailSvc.BusinessLayer.Services.Interfaces;
using InvioEmailSvc.BusinessLayer.Validator;
using InvioEmailSvc.BusinessLayer.Workers;
using InvioEmailSvc.DataAccessLayer;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace InvioEmailSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var applicationOptions = await MicroservicesExtensions.GetApplicationOptionsAsync(builder.Configuration);
        var databaseConnection = await MicroservicesExtensions.GetConnectionStringFromNamingAsync(builder.Configuration, "SqlInvioEmail");

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);

        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureJsonOptions();
        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(applicationOptions, databaseConnection);

        builder.Services.AddDefaultServices(BLConstants.DefaultCorsPolicyName);
        builder.Services.AddAntiforgery();

        builder.Services.AddTransient<IMailKitEmailSenderService, MailKitEmailSenderService>();
        builder.Services.AddTransient<ISendEmailService, SendEmailService>();

        builder.Services.AddHostedService<Worker>();
        builder.Services.ConfigureMediatR<CreateEmailMessageHandler>();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureFluentValidation<CreateEmailMessageValidator>();

        builder.Services.AddOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        await app.ApplyMigrationsAsync<AppDbContext>();

        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseDefaultServices(applicationOptions, BLConstants.DefaultCorsPolicyName);
        app.UseAntiforgery();

        versionedApi.MapEndpoints();
        await app.RunAsync();
    }
}