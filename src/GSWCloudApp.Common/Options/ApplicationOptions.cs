namespace GSWCloudApp.Common.Options;

public class ApplicationOptions
{
    public string TabellaMigrazioni { get; set; } = null!;
    public bool SwaggerEnable { get; set; }
    public int MaxRetryCount { get; set; }
    public int MaxRetryDelaySeconds { get; set; }
}