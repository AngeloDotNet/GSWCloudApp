using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring services with settings from the configuration.
/// </summary>
public static class IConfigurationExtensions
{
    /// <summary>
    /// Configures a service with settings from the specified configuration section and returns the settings.
    /// </summary>
    /// <typeparam name="T">The type of the settings.</typeparam>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configuration">The configuration to get the settings from.</param>
    /// <param name="sectionName">The name of the configuration section.</param>
    /// <returns>The settings of type <typeparamref name="T"/>.</returns>
    public static T? ConfigureAndGet<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class
    {
        var section = configuration.GetSection(sectionName);
        var settings = section.Get<T>();

        services.Configure<T>(section);

        return settings;
    }
}
