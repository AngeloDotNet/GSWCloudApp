namespace GSWCloudApp.Common.Options;

/// <summary>
/// Represents the configuration options for Redis.
/// </summary>
public class RedisOptions
{
    /// <summary>
    /// Gets or sets the hostname of the Redis server.
    /// </summary>
    public string Hostname { get; set; } = null!;

    /// <summary>
    /// Gets or sets the instance name of the Redis server.
    /// </summary>
    public string InstanceName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the absolute expiration time for cache entries.
    /// </summary>
    public TimeSpan AbsoluteExpireTime { get; set; }

    /// <summary>
    /// Gets or sets the sliding expiration time for cache entries.
    /// </summary>
    public TimeSpan SlidingExpireTime { get; set; }
}
