using GSWCloudApp.Common.ServiceGenerics.Services;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Services;

public static class DependencyInjection
{
    /// <summary>
    /// Configures generic services.
    /// </summary>
    /// <param name="services">The service collection to add the generic services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureGenericServices(this IServiceCollection services)
        => services
            .AddTransient<IGenericService, GenericService>()
            //.AddTransient<ICachedGenericService, CachedGenericService>()
            ;
}