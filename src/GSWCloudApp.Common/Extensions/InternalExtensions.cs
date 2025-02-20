using System.Text.Json;
using ConfigurazioniSvc.Shared;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Enums;
using GSWCloudApp.Common.Identity;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;
using Microsoft.Extensions.Configuration;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides internal extension methods for configuration and application settings.
/// </summary>
internal static class InternalExtensions
{
    /// <summary>
    /// Sets the application environment based on the configuration and application name.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="application">The name of the application.</param>
    /// <returns>The server API address based on the environment and application name.</returns>
    internal static string SetApplicationEnv(IConfiguration configuration, string application)
    {
        var serverApi = string.Empty;
        var environment = GetEnvironment(configuration);

        if (environment is "development" or "production")
        {
            serverApi = application switch
            {
                MicroservicesName.ConfigurazioneSmtpSvc
                    => environment == "development" ? ServiceAddress.BaseAddress_ConfigurazioneSmtpSvc : ServiceAddress.Docker_ConfigurazioneSmtpSvc,

                MicroservicesName.ConfigurazioniSvc
                    => environment == "development" ? ServiceAddress.BaseAddress_ConfigurazioniSvc : ServiceAddress.Docker_ConfigurazioniSvc,

                _ => string.Empty
            };
        }

        return serverApi;
    }

    /// <summary>
    /// Gets the current environment from the configuration.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>The current environment as a string.</returns>
    internal static string GetEnvironment(IConfiguration configuration)
    {
        return configuration["ASPNETCORE_ENVIRONMENT"]?.ToLowerInvariant() ?? string.Empty;
    }

    /// <summary>
    /// Gets the permission policies as a dictionary.
    /// </summary>
    /// <returns>A dictionary containing the permission policies and their corresponding role names.</returns>
    internal static Dictionary<TipoPolicy, string> GetPermissionPolicies()
    {
        return new Dictionary<TipoPolicy, string>
        {
            { TipoPolicy.Administrator, RoleNames.Administrator },
            { TipoPolicy.PowerUser, RoleNames.PowerUser },
            { TipoPolicy.User, RoleNames.User }
        };
    }

    /// <summary>
    /// Gets the configuration app JSON asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>A tuple containing the HttpClient and the deserialized ConfigurationApp object.</returns>
    internal static async Task<(HttpClient httpClient, ConfigurationApp deserializeResponse)> GetConfigurationAppJsonAsync(IConfiguration configuration)
    {
        var serverApi = SetApplicationEnv(configuration, MicroservicesName.ConfigurazioniSvc);
        var endpointUrl = string.Concat(serverApi, EndpointAPI.Configurazioni);

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(endpointUrl);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var deserializeResponse = JsonSerializer.Deserialize<ConfigurationApp>(responseContent, Helpers.JsonHelpers.JsonSerializer())!;

        return (httpClient, deserializeResponse);
    }

    /// <summary>
    /// Gets the connection string from the naming asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="nameConnectionString">The name of the connection string.</param>
    /// <returns>The connection string.</returns>
    internal static async Task<string> GetConnectionStringFromNamingAsync(IConfiguration configuration, string nameConnectionString)
    {
        var result = string.Empty;
        var (httpClient, deserializeResponse) = await GetConfigurationAppJsonAsync(configuration);

        switch (nameConnectionString)
        {
            case "SqlAutentica":
                result = deserializeResponse.ConnectionStrings.SqlAutentica;
                break;
            case "SqlConfigSmtp":
                result = deserializeResponse.ConnectionStrings.SqlConfigSmtp;
                break;
            case "SqlGestDocumenti":
                result = deserializeResponse.ConnectionStrings.SqlGestDocumenti;
                break;
            case "SqlGestLoghi":
                result = deserializeResponse.ConnectionStrings.SqlGestLoghi;
                break;
            case "SqlInvioEmail":
                result = deserializeResponse.ConnectionStrings.SqlInvioEmail;
                break;
            default:
                break;
        }

        return result!;
    }

    /// <summary>
    /// Gets a specific type of configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of configuration to get.</typeparam>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>The configuration object of the specified type.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified type is not found or is null.</exception>
    internal static async Task<T> GetTAsync<T>(IConfiguration configuration) where T : class
    {
        var (httpClient, deserializeResponse) = await GetConfigurationAppJsonAsync(configuration);

        if (typeof(T) == typeof(WorkerSettings))
        {
            return deserializeResponse.WorkerSettings as T ?? throw new InvalidOperationException("WorkerSettings is null");
        }
        else if (typeof(T) == typeof(ConfigurationApp))
        {
            return deserializeResponse as T ?? throw new InvalidOperationException("ConfigurationApp is null");
        }
        else if (typeof(T) == typeof(ApplicationOptions))
        {
            return deserializeResponse.ApplicationOptions as T ?? throw new InvalidOperationException("ApplicationOptions is null");
        }
        else if (typeof(T) == typeof(JwtOptions))
        {
            return deserializeResponse.JwtOptions as T ?? throw new InvalidOperationException("JwtOptions is null");
        }
        else if (typeof(T) == typeof(PollyPolicyOptions))
        {
            return deserializeResponse.PollyPolicyOptions as T ?? throw new InvalidOperationException("PollyPolicyOptions is null");
        }
        else
        {
            return deserializeResponse.ApplicationOptions as T ?? throw new InvalidOperationException("ApplicationOptions is null");
        }
    }
}
