using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Exceptions;
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
        var serverApi = SetApplicationEnv(configuration, MicroservicesName.ConfigurazioneSmtpSvc);
        var endpointApi = string.Concat(serverApi, endpoint);

        return await GetSettingsAsync<T>(httpClient, endpointApi);
    }

    /// <summary>
    /// Sets the application environment based on the configuration and application name.
    /// </summary>
    /// <param name="configuration">The configuration object.</param>
    /// <param name="application">The name of the application.</param>
    /// <returns>The server API address based on the environment and application name.</returns>
    public static string SetApplicationEnv(IConfiguration configuration, string application)
    {
        var serverApi = string.Empty;
        var environment = configuration["ASPNETCORE_ENVIRONMENT"]?.ToLowerInvariant();

        if (environment == "development" || environment == "production")
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
}
