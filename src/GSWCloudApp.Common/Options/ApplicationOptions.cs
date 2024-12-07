namespace GSWCloudApp.Common.Options;

public class ApplicationOptions
{
    /// <summary>
    /// Gets or sets the name of the migration table.
    /// </summary>
    public string TabellaMigrazioni { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether Swagger is enabled.
    /// </summary>
    public bool SwaggerEnable { get; set; }

    /// <summary>
    /// Gets or sets the maximum retry count.
    /// </summary>
    public int MaxRetryCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum retry delay in seconds.
    /// </summary>
    public int MaxRetryDelaySeconds { get; set; }
}
