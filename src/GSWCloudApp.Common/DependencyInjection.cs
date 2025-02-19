using AutoMapper;
using FluentValidation;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.RedisCache.Services;
using GSWCloudApp.Common.ServiceGenerics.Services;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace GSWCloudApp.Common;

public static class DependencyInjection
{
    /// <summary>
    /// Configures AutoMapper with the specified profile.
    /// </summary>
    /// <typeparam name="TMapProfile">The type of the AutoMapper profile.</typeparam>
    /// <param name="services">The service collection to add the AutoMapper configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureAutoMapper<TMapProfile>(this IServiceCollection services) where TMapProfile : Profile
        => services.AddAutoMapper(typeof(TMapProfile).Assembly);

    /// <summary>
    /// Configures MediatR with the specified handler.
    /// </summary>
    /// <typeparam name="THandler">The type of the MediatR handler.</typeparam>
    /// <param name="services">The service collection to add the MediatR configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureMediator<THandler>(this IServiceCollection services) where THandler : class
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(THandler).Assembly));

    /// <summary>
    /// Gets a Polly retry policy with the specified options.
    /// </summary>
    /// <param name="logger">The logger to use for logging retry attempts.</param>
    /// <param name="options">The options for configuring the retry policy.</param>
    /// <returns>The configured Polly retry policy.</returns>
    public static AsyncRetryPolicy GetRetryPolicy(ILogger logger, PollyPolicyOptions options)
    {
        return Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryCount: options.RetryCount, sleepDurationProvider: attempt
                => TimeSpan.FromSeconds(Math.Pow(options.SleepDuration, attempt)),
                onRetry: (exc, timespan, attempt, context)
                    => logger.LogWarning(exc, "Tentativo {Attempt} fallito. Riprovo tra {TimespanSeconds} secondi.", attempt, timespan.TotalSeconds));
    }

    /// <summary>
    /// Configures the Redis cache services.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="redisOptions">The options for configuring the Redis cache.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, RedisOptions redisOptions)
    {
        return services
            .AddSingleton<ICacheService, CacheService>()
            .AddStackExchangeRedisCache(action =>
            {
                action.Configuration = redisOptions.Hostname;
                action.InstanceName = redisOptions.InstanceName;
            });
    }

    //TODO: Dependency Injection FusionCache

    /// <summary>
    /// Configures generic services.
    /// </summary>
    /// <param name="services">The service collection to add the generic services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureGenericServices(this IServiceCollection services)
        => services
            .AddTransient<IGenericService, GenericService>()
            //.AddTransient<ICachedGenericService, CachedGenericService>()
            ;

    /// <summary>
    /// Configures FluentValidation with the specified validator.
    /// </summary>
    /// <typeparam name="TValidator">The type of the FluentValidation validator.</typeparam>
    /// <param name="services">The service collection to add the FluentValidation configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureFluentValidation<TValidator>(this IServiceCollection services) where TValidator : IValidator
        => services.AddValidatorsFromAssemblyContaining<TValidator>();
}
