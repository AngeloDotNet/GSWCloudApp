using MailKit.Security;

namespace GSWCloudApp.Common.Settings.SendEmail;

/// <summary>
/// Represents the settings required to configure an SMTP client.
/// </summary>
public class SmtpSettings
{
    /// <summary>
    /// Gets or sets the SMTP server host.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the port number to use for the SMTP server.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the security options for the SMTP connection.
    /// </summary>
    public SecureSocketOptions Security { get; set; }

    /// <summary>
    /// Gets or sets the username for the SMTP server authentication.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password for the SMTP server authentication.
    /// </summary>
    public string? Password { get; set; }
}
