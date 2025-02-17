namespace GSWCloudApp.Common.Constants;

/// <summary>
/// Provides service addresses for different environments.
/// </summary>
public static class ServiceAddress
{
    /// <summary>
    /// Base address for Configurazioni service in development environment.
    /// </summary>
    public const string BaseAddress_ConfigurazioniSvc = "http://localhost:5095";

    /// <summary>
    /// Base address for Configurazione SMTP service in development environment.
    /// </summary>
    public const string BaseAddress_ConfigurazioneSmtpSvc = "http://localhost:5172";

    /// <summary>
    /// Base address for Configurazioni service in production environment.
    /// </summary>
    public const string Docker_ConfigurazioniSvc = "http://api-configurazioni:5001";

    /// <summary>
    /// Base address for Configurazione SMTP service in production environment.
    /// </summary>
    public const string Docker_ConfigurazioneSmtpSvc = "http://api-configurazionesmtp:5001";
}
