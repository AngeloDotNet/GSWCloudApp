using Asp.Versioning;
using ConfigurazioniSvc.BusinessLayer.Mapper;
using ConfigurazioniSvc.BusinessLayer.Service;
using ConfigurazioniSvc.BusinessLayer.Validation;
using ConfigurazioniSvc.DataAccessLayer;
using FluentValidation;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.RedisCache;
using GSWCloudApp.Common.RedisCache.Options;
using GSWCloudApp.Common.Swagger;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace ConfigurazioniSvc.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureDbContextAsync(this IServiceCollection services,
        //IServiceProvider memoryProvider, IServiceProvider postgresProvider,
        string databaseConnection, ApplicationOptions applicationOptions)
    {
        var assembly = typeof(Program).Assembly.GetName().Name!.ToString();
        //var databaseInMemory = string.Concat(assembly, "-InMemory-Test");
        var AssemblyMigrazioni = string.Concat(assembly, ".Migrations");

        //services.AddDbContext<AppDbContext>(optionsBuilder =>
        //{
        //    optionsBuilder.UseInMemoryDatabase(databaseInMemory).UseInternalServiceProvider(memoryProvider);
        //});

        services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseNpgsql(databaseConnection, options =>
            {
                options.MigrationsAssembly(AssemblyMigrazioni)
                    .MigrationsHistoryTable(applicationOptions.TabellaMigrazioni)
                    .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            })
            //.UseInternalServiceProvider(postgresProvider)
            ;
        });

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder
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
            .ConfigureOptions<ConfigureSwaggerGenOptions>()
            .AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerDefaultValues>();
            });
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

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        return services
            .AddAntiforgery()
            .AddAutoMapper(typeof(ConfigurazioneMappingProfile).Assembly)
            .AddValidatorsFromAssemblyContaining<CreateConfigurazioneValidator>()

            .AddSingleton<ICacheService, CacheService>()
            .AddTransient<IGenericService, GenericService>()
            //.AddTransient<IConfigurazioneService, ConfigurazioneService>()
            ;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<RouteOptions>(options => options.LowercaseUrls = true)
            .Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));
    }
}