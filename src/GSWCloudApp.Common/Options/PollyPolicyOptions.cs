namespace GSWCloudApp.Common.Options;

/// <summary>
/// Represents the options for Polly policies.
/// </summary>
public class PollyPolicyOptions
{
    /// <summary>
    /// Gets or sets the number of retry attempts.
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// Gets or sets the sleep duration in seconds between retries.
    /// </summary>
    public int SleepDuration { get; set; }
}
