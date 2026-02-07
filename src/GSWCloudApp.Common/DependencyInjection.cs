using FluentValidation;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
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
    /// Configures MediatR by scanning for query and command handlers in the specified assembly.
    /// </summary>
    /// <typeparam name="TAssembly">The type from the assembly to scan for handlers.</typeparam>
    /// <param name="services">The service collection to add the handlers to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureMediatR<TAssembly>(this IServiceCollection services) where TAssembly : class
    {
        return services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection), typeof(TAssembly))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
    }

    /// <summary>
    /// Gets a Polly retry policy with the specified options.
    /// </summary>
    /// <param name="logger">The logger to use for logging retry attempts.</param>
    /// <param name="options">The options for configuring the retry policy.</param>
    /// <returns>The configured Polly retry policy.</returns>
    public static AsyncRetryPolicy GetRetryPolicy(ILogger logger, PollyPolicyOptions options)
    {
        return Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryCount: options.RetryCount, sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(options.SleepDuration, attempt)),
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
        => services.AddTransient<IGenericService, GenericService>();

    /// <summary>
    /// Configures FluentValidation with the specified validator.
    /// </summary>
    /// <typeparam name="TValidator">The type of the FluentValidation validator.</typeparam>
    /// <param name="services">The service collection to add the FluentValidation configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureFluentValidation<TValidator>(this IServiceCollection services) where TValidator : IValidator
        => services.AddValidatorsFromAssemblyContaining<TValidator>();
}
