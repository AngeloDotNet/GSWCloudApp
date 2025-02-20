using ConfigurazioniSvc.BusinessLayer.Services;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using Microsoft.OpenApi.Models;

namespace ConfigurazioniSvc.Endpoints;

public class ConfigurazioniEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IConfigurazioniService>();

        var apiGroup = endpoints
            .MapGroup("/configurazioni")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Configurazioni" }];

                return opt;
            });

        apiGroup.MapGet(MinimalApi.PatternEmpty, apiService.GetConfigurationsAsync)
            .Produces<string>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get all configurations";
                opt.Description = "Extracts all microservices configurations";

                return opt;
            });
    }
}
