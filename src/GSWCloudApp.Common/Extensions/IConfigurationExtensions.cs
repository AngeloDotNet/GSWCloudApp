using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Extensions;

public static class IConfigurationExtensions
{
    public static T? ConfigureAndGet<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class
    {
        var section = configuration.GetSection(sectionName);
        var settings = section.Get<T>();

        services.Configure<T>(section);

        return settings;
    }
}