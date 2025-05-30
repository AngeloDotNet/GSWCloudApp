﻿namespace GSWCloudApp.Common.Constants;

/// <summary>
/// Provides API endpoint constants for the application.
/// </summary>
public static class EndpointAPI
{
    /// <summary>
    /// The base path for the API.
    /// </summary>
    internal const string PathAPI = "/api/v1";

    /// <summary>
    /// The endpoint for SMTP configurations.
    /// </summary>
    public const string ConfigurazioniSmtp = $"{PathAPI}/configurazionismtp";

    /// <summary>
    /// The endpoint for sender configurations.
    /// </summary>
    public const string ConfigurazioniSender = $"{PathAPI}/configurazionisender";

    /// <summary>
    /// The endpoint for general configurations.
    /// </summary>
    public const string Configurazioni = $"{PathAPI}/configurazioni";
}
