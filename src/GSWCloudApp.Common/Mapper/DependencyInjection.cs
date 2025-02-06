using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Mapper;

public static class DependencyInjection
{
    /// <summary>
    /// Configures AutoMapper with the specified profile.
    /// </summary>
    /// <typeparam name="TMapProfile">The type of the AutoMapper profile.</typeparam>
    /// <param name="services">The service collection to add the AutoMapper configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureAutoMapper<TMapProfile>(this IServiceCollection services) where TMapProfile : Profile
        => services.AddAutoMapper(typeof(TMapProfile).Assembly);
}
