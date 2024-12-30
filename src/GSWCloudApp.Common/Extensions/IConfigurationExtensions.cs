using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Identity.Options;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.RedisCache.Options;
using Microsoft.AspNetCore.Builder;
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
    public static T? ConfigureAndGet<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
        where T : class
    {
        var section = configuration.GetSection(sectionName);
        var settings = section.Get<T>();

        services.Configure<T>(section);

        return settings;
    }

    /// <summary>
    /// Gets the database connection string from the configuration.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <param name="nameConnection">The name of the connection string.</param>
    /// <returns>The database connection string.</returns>
    /// <exception cref="DatabaseInvalidException">Thrown when the connection string is not valid.</exception>
    public static string GetDatabaseConnection(WebApplicationBuilder builder, string nameConnection)
    {
        return builder.Configuration.GetConnectionString(nameConnection)
            ?? throw new DatabaseInvalidException("Connection database string not valid.");
    }

    /// <summary>
    /// Gets the application options from the configuration.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The application options.</returns>
    /// <exception cref="OptionsInvalidException">Thrown when the application options configuration is not valid.</exception>
    public static ApplicationOptions GetApplicationOptions(WebApplicationBuilder builder)
    {
        return builder.Services.ConfigureAndGet<ApplicationOptions>(builder.Configuration, nameof(ApplicationOptions))
            ?? throw new OptionsInvalidException("Application options configuration is not valid.");
    }

    /// <summary>
    /// Gets the Redis options from the configuration.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    /// <returns>The Redis options.</returns>
    /// <exception cref="OptionsInvalidException">Thrown when the Redis options configuration is not valid.</exception>
    public static RedisOptions GetRedisOptions(WebApplicationBuilder builder)
    {
        return builder.Services.ConfigureAndGet<RedisOptions>(builder.Configuration, nameof(RedisOptions))
            ?? throw new OptionsInvalidException("Redis options configuration is not valid.");
    }

    public static JwtOptions GetJwtOptions(WebApplicationBuilder builder)
    {
        return builder.Services.ConfigureAndGet<JwtOptions>(builder.Configuration, nameof(JwtOptions))
            ?? throw new OptionsInvalidException("JWT options configuration is not valid.");
    }
}
