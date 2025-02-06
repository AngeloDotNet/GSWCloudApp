using System.Reflection;
using GSWCloudApp.Common.Routing;
using Microsoft.AspNetCore.Routing;

namespace GSWCloudApp.Common.Routing;

/// <summary>
/// Provides extension methods for mapping endpoints.
/// </summary>
public static class IEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps endpoints from the calling assembly.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <param name="predicate">An optional predicate to filter types.</param>
    public static void MapEndpoints(this IEndpointRouteBuilder endpoints, Func<Type, bool>? predicate = null)
        => endpoints.MapEndpoints(Assembly.GetCallingAssembly(), predicate);

    /// <summary>
    /// Maps endpoints from the specified assembly.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <param name="assembly">The assembly to scan for endpoint route handler builders.</param>
    /// <param name="predicate">An optional predicate to filter types.</param>
    public static void MapEndpoints(this IEndpointRouteBuilder endpoints, Assembly assembly, Func<Type, bool>? predicate = null)
    {
        ArgumentNullException.ThrowIfNull(endpoints);
        ArgumentNullException.ThrowIfNull(assembly);

        var endpointRouteHandlerBuilderInterfaceType = typeof(IEndpointRouteHandlerBuilder);

        var endpointRouteHandlerBuilderTypes = assembly.GetTypes().Where(t =>
            t.IsClass && !t.IsAbstract && !t.IsGenericType
            && endpointRouteHandlerBuilderInterfaceType.IsAssignableFrom(t) && (predicate?.Invoke(t) ?? true)).ToArray();

        foreach (var endpointRouteHandlerBuilderType in endpointRouteHandlerBuilderTypes)
        {
            var mapEndpointsMethod = endpointRouteHandlerBuilderType.GetMethod(nameof(IEndpointRouteHandlerBuilder.MapEndpoints),
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            mapEndpointsMethod?.Invoke(null, [endpoints]);
        }
    }

    /// <summary>
    /// Maps endpoints from the assembly containing the specified type.
    /// </summary>
    /// <typeparam name="T">The type whose containing assembly to scan for endpoint route handler builders.</typeparam>
    /// <param name="endpoints">The endpoint route builder.</param>
    /// <param name="predicate">An optional predicate to filter types.</param>
    public static void MapEndpointsFromAssemblyContaining<T>(this IEndpointRouteBuilder endpoints, Func<Type, bool>? predicate = null)
        where T : class => endpoints.MapEndpoints(typeof(T).Assembly, predicate);

    /// <summary>
    /// Maps endpoints using the specified endpoint route handler builder type.
    /// </summary>
    /// <typeparam name="T">The endpoint route handler builder type.</typeparam>
    /// <param name="endpoints">The endpoint route builder.</param>
    public static void MapEndpoints<T>(this IEndpointRouteBuilder endpoints)
        where T : IEndpointRouteHandlerBuilder => T.MapEndpoints(endpoints);
}
