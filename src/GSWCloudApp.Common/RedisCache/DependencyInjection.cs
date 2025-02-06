using GSWCloudApp.Common.RedisCache.Options;
using GSWCloudApp.Common.RedisCache.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.RedisCache;

public static class DependencyInjection
{
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
}
