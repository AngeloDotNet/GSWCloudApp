using ConfigurazioniSvc.BusinessLayer.Service;
using ConfigurazioniSvc.Shared.DTO;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation;

namespace ConfigurazioniSvc.Endpoints;

public class ConfigurazioniEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IConfigurazioneService>();

        var apiGroup = endpoints
            .MapGroup("/configurazioni")
            .MapToApiVersion(1)
            .WithTags("Configurazioni")
            //.RequireAuthorization()
            ;

        apiGroup.MapGet(string.Empty, apiService.GetAllAsync)
            .Produces<List<ConfigurazioneDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get all configurations";
                opt.Description = "Extracts all configurations present in the database";

                return opt;
            });

        apiGroup.MapGet(MinimalAPI.PatternById, apiService.GetByIdAsync)
            .Produces<ConfigurazioneDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get configuration by id";
                opt.Description = "Extracts the configuration from the database with the given id";

                return opt;
            });

        apiGroup.MapPost(string.Empty, apiService.PostAsync)
            .Produces<ConfigurazioneDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithValidation<CreateConfigurazioneDto>()
            .WithOpenApi(opt =>
            {
                opt.Summary = "Create a new configuration";
                opt.Description = "Create a new configuration record";

                return opt;
            });

        apiGroup.MapPatch(MinimalAPI.PatternById, apiService.UpdateAsync)
            .Produces<ConfigurazioneDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithValidation<EditConfigurazioneDto>()
            .WithOpenApi(opt =>
            {
                opt.Summary = "Update a configuration";
                opt.Description = "Edit a configuration record";

                return opt;
            });

        apiGroup.MapDelete(MinimalAPI.PatternById, apiService.DeleteAsync)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Delete a configuration";
                opt.Description = "Soft delete a configuration record";

                return opt;
            });

        apiGroup.MapGet(MinimalAPI.PatternFilterById, apiService.FilterAsync)
            .Produces<ConfigurazioneDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Filter configuration by festaId";
                opt.Description = "Filter the configuration from the database with the given festaId";

                return opt;
            });
    }
}