using GestioneDocumentiSvc.BusinessLayer.Services;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

namespace GestioneDocumentiSvc.Endpoints;

public class GestioneDocumentiEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IUploadService>();

        var apiGroup = endpoints
            .MapGroup("/documenti")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Gestione Documenti" }];

                return opt;
            })
            .DisableAntiforgery();

        apiGroup.MapPost(MinimalApi.PatternUpload, apiService.UploadFileAsync)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Upload document to server";
                opt.Description = "Upload document to server";

                return opt;
            });

        apiGroup.MapGet(MinimalApi.PatternDownload, apiService.DownloadFileAsync)
            .Produces<FileStreamHttpResult>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Download document from server";
                opt.Description = "Download document from server";

                return opt;
            });

        apiGroup.MapDelete(MinimalApi.PatternDeleteDocument, apiService.DeleteFileAsync)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Delete document from server";
                opt.Description = "Delete document from server";
                return opt;
            });
    }
}