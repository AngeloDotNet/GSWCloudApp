using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConfigurazioniSvc.Shared;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;
using Microsoft.Extensions.Configuration;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides extension methods for microservices.
/// </summary>
public static class MicroservicesExtensions
{
    /// <summary>
    /// Retrieves settings asynchronously from the specified endpoint.
    /// </summary>
    /// <typeparam name="T">The type of the settings to retrieve.</typeparam>
    /// <param name="httpClient">The HTTP client to use for the request.</param>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="endpoint">The endpoint to retrieve the settings from.</param>
    /// <returns>The settings retrieved from the endpoint.</returns>
    /// <exception cref="EndpointUnreachableException">Thrown when the endpoint is unreachable.</exception>
    /// <exception cref="InvalidResponseApiException">Thrown when the response from the API is invalid.</exception>
    public static async Task<T> GetSettingsAsync<T>(HttpClient httpClient, IConfiguration configuration, string endpoint) where T : class
    {
        var serverApi = InternalExtensions.SetApplicationEnv(configuration, MicroservicesName.ConfigurazioneSmtpSvc);
        var endpointApi = string.Concat(serverApi, endpoint);

        return await GetSettingsAsync<T>(httpClient, endpointApi);
    }

    /// <summary>
    /// Retrieves settings asynchronously from the specified endpoint.
    /// </summary>
    /// <typeparam name="T">The type of the settings to retrieve.</typeparam>
    /// <param name="httpClient">The HTTP client to use for the request.</param>
    /// <param name="endpoint">The endpoint to retrieve the settings from.</param>
    /// <returns>The settings retrieved from the endpoint.</returns>
    /// <exception cref="EndpointUnreachableException">Thrown when the endpoint is unreachable.</exception>
    /// <exception cref="InvalidResponseApiException">Thrown when the response from the API is invalid.</exception>
    public static async Task<T> GetSettingsAsync<T>(HttpClient httpClient, string endpoint) where T : class
    {
        var responseApi = await httpClient.GetAsync(endpoint);

        if (!responseApi.IsSuccessStatusCode)
        {
            throw new EndpointUnreachableException(endpoint);
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var result = await responseApi.Content.ReadFromJsonAsync<List<T>>(options) ?? throw new InvalidResponseApiException();

        return result!.FirstOrDefault()!;
    }

    /// <summary>
    /// Retrieves the connection string from the naming configuration asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="nameConnectionString">The name of the connection string.</param>
    /// <returns>The connection string.</returns>
    public static async Task<string> GetConnectionStringFromNamingAsync(IConfiguration configuration, string nameConnectionString)
        => await InternalExtensions.GetConnectionStringFromNamingAsync(configuration, nameConnectionString);

    /// <summary>
    /// Retrieves the application configuration asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>The application configuration.</returns>
    public static async Task<ConfigurationApp> GetConfigurationAppAsync(IConfiguration configuration)
        => await InternalExtensions.GetTAsync<ConfigurationApp>(configuration);

    /// <summary>
    /// Retrieves the application options asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>The application options.</returns>
    public static async Task<ApplicationOptions> GetApplicationOptionsAsync(IConfiguration configuration)
        => await InternalExtensions.GetTAsync<ApplicationOptions>(configuration);

    /// <summary>
    /// Retrieves the worker settings asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>The worker settings.</returns>
    public static async Task<WorkerSettings> GetWorkerSettingsAsync(IConfiguration configuration)
        => await InternalExtensions.GetTAsync<WorkerSettings>(configuration);

    /// <summary>
    /// Retrieves the JWT options asynchronously.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <returns>The JWT options.</returns>
    public static async Task<JwtOptions> GetJwtOptionsAsync(IConfiguration configuration)
        => await InternalExtensions.GetTAsync<JwtOptions>(configuration);

    public static async Task<PollyPolicyOptions> GetPollyPolicyOptionsAsync(IConfiguration configuration)
        => await InternalExtensions.GetTAsync<PollyPolicyOptions>(configuration);
}
