using ConfigurazioniSvc.BusinessLayer.Extensions;
using GSWCloudApp.Common.Routing;
using Microsoft.OpenApi.Models;
using Constants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioniSvc.Endpoints;

public class ConfigurazioniEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints
            .MapGroup("/configurazioni")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Configurazioni" }];

                return opt;
            });

        apiGroup.MapGet(string.Empty, IResult (IConfiguration configuration) =>
        {
            var fileName = Constants.JsonConfigurations;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            ConfigurationAppExtensions.RigenerateJSON(configuration, filePath);
            var fileBytes = File.ReadAllBytes(filePath);

            return Results.File(fileBytes, "application/json", fileName);
        })
        .Produces<string>(StatusCodes.Status200OK)
        .WithOpenApi(opt =>
        {
            opt.Summary = "Get all configurations";
            opt.Description = "Extracts all microservices configurations";

            return opt;
        });
    }
}
