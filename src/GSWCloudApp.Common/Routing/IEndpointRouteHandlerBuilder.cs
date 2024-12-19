using Microsoft.AspNetCore.Routing;

namespace GSWCloudApp.Common.Routing;

/// <summary>
/// Defines a contract for mapping endpoints.
/// </summary>
public interface IEndpointRouteHandlerBuilder
{
    /// <summary>
    /// Maps endpoints using the specified endpoint route builder.
    /// </summary>
    /// <param name="endpoints">The endpoint route builder.</param>
    public static abstract void MapEndpoints(IEndpointRouteBuilder endpoints);
}
