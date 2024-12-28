using ConfigurazioniSvc.DataAccessLayer.Entities;
using ConfigurazioniSvc.Shared.DTO;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Services;
using GSWCloudApp.Common.Validation;
using Microsoft.OpenApi.Models;

namespace ConfigurazioniSvc.Endpoints;

public class ConfigurazioniEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IGenericService>();

        var apiGroup = endpoints
            .MapGroup("/configurazioni")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Configurazioni" }];

                return opt;
            });

        apiGroup.MapGet(string.Empty, apiService.GetAllAsync<Configurazione, ConfigurazioneDto>)
            .Produces<List<ConfigurazioneDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get all configurations";
                opt.Description = "Extracts all configurations present in the database";

                return opt;
            });

        apiGroup.MapGet(MinimalApi.PatternById, apiService.GetByIdAsync<Configurazione, ConfigurazioneDto>)
            .Produces<ConfigurazioneDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get configuration by id";
                opt.Description = "Extracts the configuration from the database with the given id";

                return opt;
            });

        apiGroup.MapPost(string.Empty, apiService.PostAsync<Configurazione, ConfigurazioneDto, CreateConfigurazioneDto>)
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

        apiGroup.MapPatch(MinimalApi.PatternById, apiService.UpdateAsync<Configurazione, ConfigurazioneDto, EditConfigurazioneDto>)
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

        apiGroup.MapDelete(MinimalApi.PatternById, apiService.DeleteAsync<Configurazione>)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Delete a configuration";
                opt.Description = "Soft delete a configuration record";

                return opt;
            });

        apiGroup.MapGet(MinimalApi.PatternFilterById, apiService.FilterByIdFestaAsync<Configurazione, ConfigurazioneDto>)
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
