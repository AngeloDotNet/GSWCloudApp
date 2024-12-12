﻿using System.Text;
using System.Text.Json.Serialization;
using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Requirements;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.RedisCache;
using GSWCloudApp.Common.RedisCache.Options;
using GSWCloudApp.Common.Services;
using GSWCloudApp.Common.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Configures the database context with the specified options.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="databaseConnection">The database connection string.</param>
    /// <param name="applicationOptions">The application options.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureDbContextAsync<T, TDbContext>(this IServiceCollection services, string databaseConnection,
        ApplicationOptions applicationOptions) where T : class
        where TDbContext : DbContext
    {
        var assembly = typeof(T).Assembly.GetName().Name!.ToString();
        var AssemblyMigrazioni = string.Concat(assembly, ".Migrations");

        services.AddScoped<DbContext, TDbContext>();
        services.AddDbContext<TDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(databaseConnection, options =>
            {
                options.MigrationsAssembly(AssemblyMigrazioni)
                    .MigrationsHistoryTable(applicationOptions.TabellaMigrazioni)
                    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            });
        });

        return services;
    }

    /// <summary>
    /// Configures CORS with the specified policy name.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="policyName">The name of the CORS policy.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureCors(this IServiceCollection services, string policyName)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder
                => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    /// <summary>
    /// Configures API versioning for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
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

        return services;
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
    /// <param name="configuration">The configuration to get the settings from.</param>
    /// <param name="redisConnection">The Redis connection string.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, RedisOptions redisOptions)
    {
        return services.AddStackExchangeRedisCache(action =>
        {
            action.Configuration = redisOptions.Hostname;
            action.InstanceName = redisOptions.InstanceName;
        });
    }
    //public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, IConfiguration configuration, string redisConnection)
    //{
    //    var options = services.ConfigureAndGet<RedisOptions>(configuration, nameof(RedisOptions))
    //        ?? throw new InvalidOperationException("Redis options not found in configuration.");

    //    options.Hostname = redisConnection;

    //    return services.AddStackExchangeRedisCache(action =>
    //    {
    //        action.Configuration = options.Hostname;
    //        action.InstanceName = options.InstanceName;
    //    });
    //}

    /// <summary>
    /// Configures services with AutoMapper and FluentValidation.
    /// </summary>
    /// <typeparam name="TMappingProfile">The type of the AutoMapper profile.</typeparam>
    /// <typeparam name="TValidator">The type of the FluentValidation validator.</typeparam>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureServices<TMappingProfile, TValidator>(this IServiceCollection services)
    where TMappingProfile : Profile
    where TValidator : IValidator
    {
        services.AddAutoMapper(typeof(TMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<TValidator>();

        // Service Registrations with Singleton Lifecycle
        services.AddSingleton<ICacheService, CacheService>();

        // Service Registrations with Transient Lifecycle
        services.AddTransient<IGenericService, GenericService>();

        return services;
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
    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));

        return services;
    }

    /// <summary>
    /// Configures JWT authentication for the application.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="jwtSettings">The JWT settings to use for configuration.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection ConfigureAuthTokenJWTShared(this IServiceCollection services, JwtOptions jwtSettings)
    {
        services.AddAuthentication(options =>
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

        services.AddScoped<IAuthorizationHandler, UserActiveHandler>();
        services.AddAuthorization(options =>
        {
            var policyBuilder = new AuthorizationPolicyBuilder().RequireAuthenticatedUser();
            policyBuilder.Requirements.Add(new UserActiveRequirement());
            options.FallbackPolicy = options.DefaultPolicy = policyBuilder.Build();
        });

        return services;
    }
}