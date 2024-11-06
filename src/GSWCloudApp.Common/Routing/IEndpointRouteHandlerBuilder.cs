using Microsoft.AspNetCore.Routing;

namespace GSWCloudApp.Common.Routing;

public interface IEndpointRouteHandlerBuilder
{
    public static abstract void MapEndpoints(IEndpointRouteBuilder endpoints);
}
