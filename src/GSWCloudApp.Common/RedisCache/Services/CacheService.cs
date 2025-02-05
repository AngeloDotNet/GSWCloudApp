using System.Text.Json;
using GSWCloudApp.Common.RedisCache.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace GSWCloudApp.Common.RedisCache.Services;

/// <summary>
/// Provides methods for interacting with the distributed cache.
/// </summary>
public class CacheService(IDistributedCache cache, IOptionsMonitor<RedisOptions> redisOptionsMonitor) : ICacheService
{
    /// <summary>
    /// Retrieves an item from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to retrieve.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <returns>The cached item, or the default value if the item is not found.</returns>
    public async Task<T> GetCacheAsync<T>(string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData is null)
        {
            return default!;
        }

        return JsonSerializer.Deserialize<T>(jsonData)!;
    }

    /// <summary>
    /// Sets an item in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The item to cache.</param>
    /// <returns>The cached item.</returns>
    public async Task<T> SetCacheAsync<T>(string key, T value)
    {
        var optionsCache = redisOptionsMonitor.CurrentValue;

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = optionsCache.AbsoluteExpireTime,
            SlidingExpiration = optionsCache.SlidingExpireTime
        };

        var jsonData = JsonSerializer.Serialize(value);

        await cache.SetStringAsync(key, jsonData, options);

        return value;
    }
}
