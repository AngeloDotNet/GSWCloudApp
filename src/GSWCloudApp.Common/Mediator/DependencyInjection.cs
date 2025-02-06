using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Mediator;

public static class DependencyInjection
{
    /// <summary>
    /// Configures MediatR with the specified handler.
    /// </summary>
    /// <typeparam name="THandler">The type of the MediatR handler.</typeparam>
    /// <param name="services">The service collection to add the MediatR configuration to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection ConfigureMediator<THandler>(this IServiceCollection services) where THandler : class
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(THandler).Assembly));
}
