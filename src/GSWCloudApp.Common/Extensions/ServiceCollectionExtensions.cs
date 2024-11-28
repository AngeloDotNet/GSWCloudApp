using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.RedisCache;
using GSWCloudApp.Common.RedisCache.Options;
using GSWCloudApp.Common.Services;
using GSWCloudApp.Common.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace GSWCloudApp.Common.Extensions;

public static class ServiceExtensions
{
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

    public static IServiceCollection ConfigureCors(this IServiceCollection services, string policyName)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(policyName, builder
                => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

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

    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        return services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                //context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
            };
        });
    }

    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, IConfiguration configuration, string redisConnection)
    {
        var options = services.ConfigureAndGet<RedisOptions>(configuration, nameof(RedisOptions))
            ?? throw new InvalidOperationException("Redis options not found in configuration.");

        options.Hostname = redisConnection;

        return services.AddStackExchangeRedisCache(action =>
        {
            action.Configuration = options.Hostname;
            action.InstanceName = options.InstanceName;
        });
    }

    //TODO: da eliminare al prossimo refactoring
    //public static IServiceCollection ConfigureServices<TDbContext, TMappingProfile, TValidator>(this IServiceCollection services)
    //    where TDbContext : DbContext
    //    where TMappingProfile : Profile
    //    where TValidator : IValidator
    //{
    //    //services.AddAntiforgery(); //Non necessario in quanto dichiarato nella Program.cs dopo il routing
    //    services.AddAutoMapper(typeof(TMappingProfile).Assembly);
    //    services.AddValidatorsFromAssemblyContaining<TValidator>();

    //    // Service Registrations with Singleton Lifecycle
    //    services.AddSingleton<ICacheService, CacheService>();

    //    // Service Registrations with Transient Lifecycle
    //    services.AddTransient<IGenericService, GenericService>();

    //    // Service Registrations with Scoped Lifecycle
    //    services.AddScoped<DbContext, TDbContext>();

    //    return services;
    //}

    public static IServiceCollection ConfigureServices<TMappingProfile, TValidator>(this IServiceCollection services)
        where TMappingProfile : Profile
        where TValidator : IValidator
    {
        //TODO: da eliminare al prossimo refactoring
        //services.AddAntiforgery(); //Non necessario in quanto dichiarato nella Program.cs dopo il routing

        services.AddAutoMapper(typeof(TMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<TValidator>();

        // Service Registrations with Singleton Lifecycle
        services.AddSingleton<ICacheService, CacheService>();

        // Service Registrations with Transient Lifecycle
        services.AddTransient<IGenericService, GenericService>();

        return services;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));

        return services;
    }
}