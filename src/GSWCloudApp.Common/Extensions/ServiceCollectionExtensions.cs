using System.Text;
using System.Text.Json.Serialization;
using Asp.Versioning;
using GSWCloudApp.Common.Identity.Entities.Application;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Identity.Requirements;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Configures Serilog for the application with the specified program type.
    /// </summary>
    /// <typeparam name="TProgram">The type of the program class.</typeparam>
    /// <param name="builder">The web application builder to configure.</param>
    public static void AddConfigurationSerilog<TProgram>(WebApplicationBuilder builder) where TProgram : class
    {
        builder.Host.UseSerilog((context, config) =>
        {
            var assemblyProject = typeof(TProgram).Assembly.GetName().Name!;
            var romeTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome"));

            config.ReadFrom.Configuration(context.Configuration);
            config.Enrich.WithProperty("Application", assemblyProject);
            config.Enrich.WithProperty("Timestamp", romeTime);
        });
    }

    /// <summary>
    /// Configures the database context for the application.
    /// </summary>
    /// <typeparam name="T">The type of the class.</typeparam>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="applicationOptions">The application options.</param>
    /// <param name="databaseConnection">The database connection string.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureDbContextAsync<T, TDbContext>(this IServiceCollection services,
        ApplicationOptions applicationOptions, string databaseConnection) where T : class where TDbContext : DbContext
    {
        var assemblyMigrations = $"{typeof(T).Assembly.GetName().Name}.Migrations";

        services.AddScoped<DbContext, TDbContext>();
        services.AddDbContext<TDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(databaseConnection, options =>
            {
                options.MigrationsAssembly(assemblyMigrations)
                    .MigrationsHistoryTable(applicationOptions.TabellaMigrazioni)
                    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .EnableRetryOnFailure(applicationOptions.MaxRetryCount, TimeSpan.FromSeconds(applicationOptions.MaxRetryDelaySeconds), null);
            });
        });

        return services;
    }

    public static IServiceCollection AddDefaultServices(this IServiceCollection services, string policyName)
    {
        services
            .AddCors(options => options.AddPolicy(policyName, builder
                => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()))

            .AddApiVersioning(options =>
            {
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.DefaultApiVersion = new ApiVersion(1);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerDefaultValues>();
            })
            .ConfigureOptions<ConfigureSwaggerGenOptions>()
            .AddAntiforgery();

        return services;
    }

    /// <summary>
    /// Configures CORS with the specified policy name.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="policyName">The name of the CORS policy.</param>
    /// <returns>The configured service collection.</returns>
    [Obsolete("This method is obsolete. Use AddDefaultServices instead.")]
    public static IServiceCollection ConfigureCors(this IServiceCollection services, string policyName)
    {
        return services.AddCors(options
            => options.AddPolicy(policyName, builder
                => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
    }

    /// <summary>
    /// Configures API versioning for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    [Obsolete("This method is obsolete. Use AddDefaultServices instead.")]
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.DefaultApiVersion = new ApiVersion(1);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    /// <summary>
    /// Configures Swagger for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    [Obsolete("This method is obsolete. Use AddDefaultServices instead.")]
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerDefaultValues>();
            })
            .ConfigureOptions<ConfigureSwaggerGenOptions>();
    }

    /// <summary>
    /// Configures Swagger with authentication for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureAuthSwagger(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert the Bearer Token",
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                options.EnableAnnotations();
                options.OperationFilter<SwaggerDefaultValues>();
            })
            .ConfigureOptions<ConfigureSwaggerGenOptions>();
    }

    /// <summary>
    /// Configures problem details for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        return services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });
    }

    /// <summary>
    /// Configures Redis cache for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="redisOptions">The Redis options.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, RedisOptions redisOptions)
    {
        return services.AddStackExchangeRedisCache(action =>
        {
            action.Configuration = redisOptions.Hostname;
            action.InstanceName = redisOptions.InstanceName;
        });
    }

    /// <summary>
    /// Configures JSON options for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services)
    {
        return services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    /// <summary>
    /// Configures options for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">The configuration to get the settings from.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<RouteOptions>(options => options.LowercaseUrls = true)
            .Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));
    }

    /// <summary>
    /// Configures JWT settings for the application.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="jwtOptions">The JWT options.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureJWTSettings<TDbContext>(this IServiceCollection services, JwtOptions jwtOptions)
        where TDbContext : DbContext
    {
        var iOptions = new SecurityOptions();
        var listPolicies = InternalExtensions.GetPermissionPolicies();
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));

        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // User validation criteria
                options.User.RequireUniqueEmail = iOptions.RequiredUniqueEmail;

                // Password validation criteria
                options.Password.RequireDigit = iOptions.RequiredDigit;
                options.Password.RequiredLength = iOptions.RequiredLenght;
                options.Password.RequireUppercase = iOptions.RequiredUppercase;
                options.Password.RequireLowercase = iOptions.RequiredLowercase;
                options.Password.RequireNonAlphanumeric = iOptions.RequiredNonAlphanumeric;
                options.Password.RequiredUniqueChars = iOptions.RequiredUniqueChars;

                // Account confirmation
                options.SignIn.RequireConfirmedEmail = iOptions.RequiredConfirmedEmail;

                // Account lockout
                options.Lockout.AllowedForNewUsers = iOptions.AllowedForNewUsers;
                options.Lockout.MaxFailedAccessAttempts = iOptions.MaxFailedAccessAttempts;
                options.Lockout.DefaultLockoutTimeSpan = iOptions.DefaultLockoutTimeSpan;
            })
            .AddEntityFrameworkStores<TDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services
            .AddScoped<IAuthorizationHandler, UserActiveHandler>()
            .AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
                policyBuilder.Requirements.Add(new UserActiveRequirement());
                options.FallbackPolicy = options.DefaultPolicy = policyBuilder.Build();

                foreach (var policy in listPolicies)
                {
                    options.AddPolicy(policy.Key.ToString(), policyBuilder => policyBuilder.RequireRole(policy.Value));
                }
            });

        return services;
    }
}
