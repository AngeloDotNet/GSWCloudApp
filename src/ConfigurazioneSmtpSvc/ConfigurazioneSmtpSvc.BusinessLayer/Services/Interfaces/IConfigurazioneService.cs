using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;

public interface IConfigurazioneService
{
    Task<Results<Ok<List<SettingSenderDto>>, BadRequest>> GetSenderConfigurationAsync(ISender sender, CancellationToken cancellationToken);
    Task<Results<Ok<SettingSenderDto>, BadRequest>> CreateSenderConfigurationAsync(CreateSettingSenderDto request, ISender sender, CancellationToken cancellationToken);
    Task<Results<Ok<SettingSenderDto>, BadRequest>> EditSenderConfigurationAsync(EditSettingSenderDto request, ISender sender, CancellationToken cancellationToken);

    Task<Results<Ok<List<SettingSmtpDto>>, BadRequest>> GetSmtpConfigurationAsync(ISender sender, CancellationToken cancellationToken);
    Task<Results<Ok<SettingSmtpDto>, BadRequest>> CreateSmtpConfigurationAsync(CreateSettingSmtpDto request, ISender sender, CancellationToken cancellationToken);
    Task<Results<Ok<SettingSmtpDto>, BadRequest>> EditSmtpConfigurationAsync(EditSettingSmtpDto request, ISender sender, CancellationToken cancellationToken);
}
