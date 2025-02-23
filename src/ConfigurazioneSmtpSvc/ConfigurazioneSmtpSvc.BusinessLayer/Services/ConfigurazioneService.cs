using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Edit;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Query;
using ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Services;

public class ConfigurazioneService : IConfigurazioneService
{
    public async Task<Results<Ok<List<SettingSenderDto>>, BadRequest>> GetSenderConfigurationAsync(ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllSettingSenderQuery(), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }
        else
        {
            return TypedResults.BadRequest();
        }
    }

    public async Task<Results<Ok<SettingSenderDto>, BadRequest>> CreateSenderConfigurationAsync(CreateSettingSenderDto request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateSettingSenderCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<SettingSenderDto>, BadRequest>> EditSenderConfigurationAsync(EditSettingSenderDto request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new EditSettingSenderCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<List<SettingSmtpDto>>, BadRequest>> GetSmtpConfigurationAsync(ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetAllSettingSmtpQuery(), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<SettingSmtpDto>, BadRequest>> CreateSmtpConfigurationAsync(CreateSettingSmtpDto request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateSettingSmtpCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<SettingSmtpDto>, BadRequest>> EditSmtpConfigurationAsync(EditSettingSmtpDto request, ISender sender, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new EditSettingSmtpCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }
}
