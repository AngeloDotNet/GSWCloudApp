namespace GSWCloudApp.Common.Settings.SendEmail;

/// <summary>
/// Represents the settings for the email sender.
/// </summary>
public class SenderSettings
{
    /// <summary>
    /// Gets or sets the name of the sender.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the sender.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
