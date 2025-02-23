using ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation.Extensions;
using Microsoft.OpenApi.Models;

namespace ConfigurazioneSmtpSvc.Endpoints;

public class ConfigurazioniSenderEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IConfigurazioneService>();

        var apiGroup = endpoints
            .MapGroup("/configurazionisender")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Configurazioni Sender" }];

                return opt;
            });

        //apiGroup.MapGet(MinimalApi.PatternEmpty, async Task<Results<Ok<List<SettingSenderDto>>, BadRequest>> (ISender sender, CancellationToken cancellationToken) =>
        //{
        //    var result = await sender.Send(new GetAllSettingSenderQuery(), cancellationToken);

        //    if (result != null)
        //    {
        //        return TypedResults.Ok(result);
        //    }
        //    else
        //    {
        //        return TypedResults.BadRequest();
        //    }
        //})
        apiGroup.MapGet(MinimalApi.PatternEmpty, apiService.GetSenderConfigurationAsync)
            .Produces<IQueryable<SettingSenderDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get all sender configurations";
                opt.Description = "Extracts all sender configurations present in the database";

                return opt;
            });

        //apiGroup.MapPost(MinimalApi.PatternEmpty, async Task<Results<Ok<SettingSenderDto>, BadRequest>> (CreateSettingSenderDto request,
        //    ISender sender, CancellationToken cancellationToken) =>
        //{
        //    var result = await sender.Send(new CreateSettingSenderCommand(request), cancellationToken);

        //    if (result != null)
        //    {
        //        return TypedResults.Ok(result);
        //    }

        //    return TypedResults.BadRequest();
        //})
        apiGroup.MapPost(MinimalApi.PatternEmpty, apiService.CreateSenderConfigurationAsync)
            .Produces<SettingSenderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithValidation<CreateSettingSenderDto>()
            .WithOpenApi(opt =>
            {
                opt.Summary = "Create a new sender configurations";
                opt.Description = "Create a new sender configurations";

                return opt;
            });

        //apiGroup.MapPatch(string.Empty, async Task<Results<Ok<SettingSenderDto>, BadRequest>> (EditSettingSenderDto request,
        //    ISender sender, CancellationToken cancellationToken) =>
        //{
        //    var result = await sender.Send(new EditSettingSenderCommand(request), cancellationToken);

        //    if (result != null)
        //    {
        //        return TypedResults.Ok(result);
        //    }

        //    return TypedResults.BadRequest();
        //})
        apiGroup.MapPatch(string.Empty, apiService.EditSenderConfigurationAsync)
            .Produces<SettingSenderDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Edit a sender configurations";
                opt.Description = "Edit a sender configurations";

                return opt;
            });
    }
}