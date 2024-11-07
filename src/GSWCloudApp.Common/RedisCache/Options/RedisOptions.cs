namespace GSWCloudApp.Common.RedisCache.Options;

public class RedisOptions
{
    public string Hostname { get; set; } = null!;
    public string InstanceName { get; set; } = null!;
    public TimeSpan AbsoluteExpireTime { get; set; }
    public TimeSpan SlidingExpireTime { get; set; }
}