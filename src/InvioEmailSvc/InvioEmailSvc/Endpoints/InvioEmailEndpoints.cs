using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation.Extensions;
using InvioEmailSvc.BusinessLayer.Services.Interfaces;
using InvioEmailSvc.Shared.DTO;
using Microsoft.OpenApi.Models;

namespace InvioEmailSvc.Endpoints;

public class InvioEmailEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints
            .MapGroup("/invioemail")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Invio Email" }];

                return opt;
            })
            .DisableAntiforgery();

        apiGroup.MapPost(MinimalApi.PatternEmpty, async (CreateEmailMessageDto request, ISendEmailService service, CancellationToken cancellationToken) =>
        {
            return await service.SendEmailAsync(request, cancellationToken);
        })
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
        .WithValidation<CreateEmailMessageDto>()
        .WithOpenApi(opt =>
        {
            opt.Summary = "Send email";
            opt.Description = "Send email process";

            return opt;
        });
    }
}