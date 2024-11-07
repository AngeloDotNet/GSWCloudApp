using System.Text.Json;
using GSWCloudApp.Common.RedisCache.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace GSWCloudApp.Common.RedisCache;

public class CacheService(IDistributedCache cache, IOptionsMonitor<RedisOptions> redisOptionsMonitor) : ICacheService
{
    //private readonly IDistributedCache cache = cache;
    //private readonly IOptionsMonitor<RedisOptions> redisOptionsMonitor = redisOptionsMonitor;

    public async Task<T> GetCacheAsync<T>(string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData is null)
        {
            return default!;
        }

        return JsonSerializer.Deserialize<T>(jsonData)!;
    }

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