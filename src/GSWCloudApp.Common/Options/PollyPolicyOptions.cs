namespace GSWCloudApp.Common.Options;

public class PollyPolicyOptions
{
    public int RetryCount { get; set; }
    public int SleepDuration { get; set; }
}
