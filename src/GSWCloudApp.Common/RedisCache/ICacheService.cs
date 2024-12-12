namespace GSWCloudApp.Common.RedisCache;

/// <summary>
/// Defines methods for interacting with the cache.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieves an item from the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to retrieve.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <returns>The cached item, or the default value if the item is not found.</returns>
    Task<T> GetCacheAsync<T>(string key);

    /// <summary>
    /// Sets an item in the cache.
    /// </summary>
    /// <typeparam name="T">The type of the item to cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The item to cache.</param>
    /// <returns>The cached item.</returns>
    Task<T> SetCacheAsync<T>(string key, T value);
}
