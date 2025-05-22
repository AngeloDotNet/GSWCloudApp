using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Edit;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Query;
using ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Services;

public class ConfigurazioneService(IQueryHandler<GetAllSettingSenderQuery, List<SettingSenderDto>> getSenderHandler,
    ICommandHandler<CreateSettingSenderCommand, SettingSenderDto> createSenderHandler,
    ICommandHandler<EditSettingSenderCommand, SettingSenderDto> editSenderHandler,
    IQueryHandler<GetAllSettingSmtpQuery, List<SettingSmtpDto>> getSmtpHandler,
    ICommandHandler<CreateSettingSmtpCommand, SettingSmtpDto> createSmtpHandler,
    ICommandHandler<EditSettingSmtpCommand, SettingSmtpDto> editSmtpHandler) : IConfigurazioneService
{
    public async Task<Results<Ok<List<SettingSenderDto>>, BadRequest>> GetSenderConfigurationAsync(CancellationToken cancellationToken)
    {
        var result = await getSenderHandler.Handle(new GetAllSettingSenderQuery(), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }
        else
        {
            return TypedResults.BadRequest();
        }
    }

    public async Task<Results<Ok<SettingSenderDto>, BadRequest>> CreateSenderConfigurationAsync(CreateSettingSenderDto request, CancellationToken cancellationToken)
    {
        var result = await createSenderHandler.Handle(new CreateSettingSenderCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<SettingSenderDto>, BadRequest>> EditSenderConfigurationAsync(EditSettingSenderDto request, CancellationToken cancellationToken)
    {
        var result = await editSenderHandler.Handle(new EditSettingSenderCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<List<SettingSmtpDto>>, BadRequest>> GetSmtpConfigurationAsync(CancellationToken cancellationToken)
    {
        var result = await getSmtpHandler.Handle(new GetAllSettingSmtpQuery(), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<SettingSmtpDto>, BadRequest>> CreateSmtpConfigurationAsync(CreateSettingSmtpDto request, CancellationToken cancellationToken)
    {
        var result = await createSmtpHandler.Handle(new CreateSettingSmtpCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }

    public async Task<Results<Ok<SettingSmtpDto>, BadRequest>> EditSmtpConfigurationAsync(EditSettingSmtpDto request, CancellationToken cancellationToken)
    {
        var result = await editSmtpHandler.Handle(new EditSettingSmtpCommand(request), cancellationToken);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.BadRequest();
    }
}
