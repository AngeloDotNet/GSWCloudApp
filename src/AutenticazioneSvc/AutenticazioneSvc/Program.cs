using System.Text;
using AutenticazioneSvc.BusinessLayer.HostedService;
using AutenticazioneSvc.BusinessLayer.Services;
using AutenticazioneSvc.DataAccessLayer;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Entities;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AutenticazioneSvc;

public class Program
{
    public static async Task Main(string[] args)
    {
        var policyCorsName = "AllowAll";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor();
        builder.Services.ConfigureJsonOptions();

        var assemblyProject = typeof(Program).Assembly.GetName().Name!.ToString().ToLower();
        var postgresConnection = await ApplicationExtensions.GetVaultStringConnectionAsync(builder, assemblyProject, "connection");

        var applicationOptions = builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration,
            nameof(ApplicationOptions)) ?? throw new InvalidOperationException("Application options not found.");

        var jwtSettings = builder.Services.ConfigureAndGet<JwtOptions>(builder.Configuration, nameof(JwtOptions))
            ?? throw new InvalidOperationException("JWT options not found.");

        builder.Services.ConfigureDbContextAsync<Program, AppDbContext>(postgresConnection, applicationOptions);
        builder.Services.ConfigureCors(policyCorsName);

        builder.Services.ConfigureApiVersioning();
        builder.Services.ConfigureAuthSwagger();

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;

            // Criteri di validazione della password
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredUniqueChars = 4;

            // Conferma dell'account
            options.SignIn.RequireConfirmedEmail = true;

            // Blocco dell'account
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        //builder.Services.ConfigureAuthTokenJWTShared(jwtSettings);
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = false;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization();
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddAntiforgery();
        builder.Services.AddHostedService<AuthStartupTask>();

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureOptions(builder.Configuration);

        var app = builder.Build();
        var versionedApi = ApplicationExtensions.UseVersioningApi(app);

        await app.ApplyMigrationsAsync<AppDbContext>();
        app.UseExceptionHandler();

        app.UseStatusCodePages();
        app.UseDevSwagger(applicationOptions);

        app.UseForwardNetworking();
        app.UseRouting();

        app.UseCors(policyCorsName);
        app.UseAntiforgery();

        app.UseAuthorization();
        versionedApi.MapEndpoints();

        await app.RunAsync();
    }
}