using ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using GSWCloudApp.Common.Constants;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation.Extensions;
using Microsoft.OpenApi.Models;

namespace ConfigurazioneSmtpSvc.Endpoints;

public class ConfigurazioniSmtpEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiService = endpoints.ServiceProvider.GetRequiredService<IConfigurazioneService>();

        var apiGroup = endpoints
            .MapGroup("/configurazionismtp")
            .MapToApiVersion(1)
            .WithOpenApi(opt =>
            {
                opt.Tags = [new OpenApiTag { Name = "Configurazioni Smtp" }];

                return opt;
            });

        //apiGroup.MapGet(MinimalApi.PatternEmpty, async Task<Results<Ok<List<SettingSmtpDto>>, BadRequest>> ([FromServices] ISender sender,
        //    CancellationToken cancellationToken) =>
        //{
        //    var result = await sender.Send(new GetAllSettingSmtpQuery(), cancellationToken);

        //    if (result != null)
        //    {
        //        return TypedResults.Ok(result);
        //    }

        //    return TypedResults.BadRequest();
        //})
        apiGroup.MapGet(MinimalApi.PatternEmpty, apiService.GetSmtpConfigurationAsync)
            .Produces<IQueryable<SettingSmtpDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Get all smtp configurations";
                opt.Description = "Extracts all smtp configurations present in the database";

                return opt;
            });

        //apiGroup.MapPost(MinimalApi.PatternEmpty, async Task<Results<Ok<SettingSmtpDto>, BadRequest>> (CreateSettingSmtpDto request,
        //    ISender sender, CancellationToken cancellationToken) =>
        //{
        //    var result = await sender.Send(new CreateSettingSmtpCommand(request), cancellationToken);

        //    if (result != null)
        //    {
        //        return TypedResults.Ok(result);
        //    }

        //    return TypedResults.BadRequest();
        //})
        apiGroup.MapPost(MinimalApi.PatternEmpty, apiService.CreateSmtpConfigurationAsync)
            .Produces<SettingSmtpDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithValidation<CreateSettingSmtpDto>()
            .WithOpenApi(opt =>
            {
                opt.Summary = "Create a new smtp configurations";
                opt.Description = "Create a new smtp configurations";

                return opt;
            });

        //apiGroup.MapPatch(MinimalApi.PatternEmpty, async Task<Results<Ok<SettingSmtpDto>, BadRequest>> (EditSettingSmtpDto request, ISender sender,
        //    CancellationToken cancellationToken) =>
        //{
        //    var result = await sender.Send(new EditSettingSmtpCommand(request), cancellationToken);

        //    if (result != null)
        //    {
        //        return TypedResults.Ok(result);
        //    }

        //    return TypedResults.BadRequest();
        //})
        apiGroup.MapPatch(MinimalApi.PatternEmpty, apiService.EditSmtpConfigurationAsync)
            .Produces<SettingSmtpDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .WithOpenApi(opt =>
            {
                opt.Summary = "Edit a smtp configurations";
                opt.Description = "Edit a smtp configurations";

                return opt;
            });
    }
}