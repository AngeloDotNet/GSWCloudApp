using AutenticazioneSvc.BusinessLayer.HostedService;
using AutenticazioneSvc.BusinessLayer.Services;
using AutenticazioneSvc.BusinessLayer.Services.Interfaces;
using AutenticazioneSvc.BusinessLayer.Validator;
using AutenticazioneSvc.DataAccessLayer;
using GSWCloudApp.Common;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Routing;
using BLConstants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace AutenticazioneSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var applicationOptions = await MicroservicesExtensions.GetApplicationOptionsAsync(builder.Configuration);

        var databaseConnection = await MicroservicesExtensions.GetConnectionStringFromNamingAsync(builder.Configuration, "SqlAutentica");
        var jwtOptions = await MicroservicesExtensions.GetJwtOptionsAsync(builder.Configuration);

        ServiceExtensions.AddConfigurationSerilog<Program>(builder);

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(applicationOptions, databaseConnection);
        builder.Services.AddDefaultServices(BLConstants.DefaultCorsPolicyName);

        builder.Services.ConfigureAuthSwagger();
        builder.Services.ConfigureJWTSettings<AppDbContext>(jwtOptions);

        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddAntiforgery();

        builder.Services.AddHostedService<AuthStartupTask>();
        builder.Services.ConfigureProblemDetails();

        builder.Services.ConfigureFluentValidation<LoginValidator>();
        builder.Services.AddOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        await app.ApplyMigrationsAsync<AppDbContext>();

        app.UseDefaultServices(applicationOptions, BLConstants.DefaultCorsPolicyName);
        app.UseAntiforgery();

        app.UseAuthorization();
        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}