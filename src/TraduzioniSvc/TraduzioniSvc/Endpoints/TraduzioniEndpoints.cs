using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using Microsoft.OpenApi.Models;
using TraduzioniSvc.BusinessLayer.Services;

namespace TraduzioniSvc.Endpoints;

public class TraduzioniEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<ITranslateService>();

        var apiGroup = endpoints
            .MapGroup("/traduzioni")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Traduzioni" }];

                return opt;
            });

        apiGroup.MapGet(MinimalApi.PatternLanguage, apiService.GetTranslationsAsync)
        .Produces<string>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithOpenApi(opt =>
        {
            opt.Summary = "Get the translations for the requested language";
            opt.Description = "Extracts all translations for the requested language";

            return opt;
        });
    }
}
