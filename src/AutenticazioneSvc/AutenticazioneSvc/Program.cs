using AutenticazioneSvc.BusinessLayer.HostedService;
using AutenticazioneSvc.BusinessLayer.Services;
using AutenticazioneSvc.BusinessLayer.Validator;
using AutenticazioneSvc.DataAccessLayer;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace AutenticazioneSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        builder.Services.ConfigureDbContext<Program, AppDbContext>(builder.Configuration, "SqlAutentica");
        builder.Services.ConfigureCors(BLConstants.DefaultCorsPolicyName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureAuthSwagger();

        builder.Services.ConfigureJWTSettings<AppDbContext>(builder.Configuration);
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddAntiforgery();
        builder.Services.AddHostedService<AuthStartupTask>();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureFluentValidation<LoginValidator>();

        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        await app.ApplyMigrationsAsync<AppDbContext>();
        app.UseExceptionHandler();

        app.UseStatusCodePages();
        app.UseDevSwagger(builder.Configuration);

        app.UseForwardNetworking();
        app.UseRouting();

        app.UseCors(BLConstants.DefaultCorsPolicyName);
        app.UseAntiforgery();

        app.UseAuthorization();
        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}