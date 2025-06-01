using GestioneLoghiSvc.BusinessLayer.Services;
using GestioneLoghiSvc.Shared.DTO;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

namespace GestioneLoghiSvc.Endpoints;

public class GestioneLoghiEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IUploadService>();

        var apiGroup = endpoints
            .MapGroup("/immagini")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Gestione Immagini" }];

                return opt;
            })
            .DisableAntiforgery();

        apiGroup.MapPost(MinimalApi.PatternUpload, apiService.UploadFileAsync)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithValidation<UploadImmagineDto>()
            .WithOpenApi(opt =>
            {
                opt.Summary = "Upload image to server";
                opt.Description = "Upload image to server";

                return opt;
            });

        apiGroup.MapGet(MinimalApi.PatternDownload, apiService.DownloadFileAsync)
            .Produces<FileStreamHttpResult>()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Download image from server";
                opt.Description = "Download image from server";

                return opt;
            });

        apiGroup.MapDelete(MinimalApi.PatternDeleteDocument, apiService.DeleteFileAsync)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Delete image from server";
                opt.Description = "Delete image from server";
                return opt;
            });
    }
}