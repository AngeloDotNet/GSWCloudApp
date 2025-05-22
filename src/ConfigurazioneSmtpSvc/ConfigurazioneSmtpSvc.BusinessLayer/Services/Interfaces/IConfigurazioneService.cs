using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Services.Interfaces;

public interface IConfigurazioneService
{
    Task<Results<Ok<List<SettingSenderDto>>, BadRequest>> GetSenderConfigurationAsync(CancellationToken cancellationToken);
    Task<Results<Ok<SettingSenderDto>, BadRequest>> CreateSenderConfigurationAsync(CreateSettingSenderDto request, CancellationToken cancellationToken);
    Task<Results<Ok<SettingSenderDto>, BadRequest>> EditSenderConfigurationAsync(EditSettingSenderDto request, CancellationToken cancellationToken);

    Task<Results<Ok<List<SettingSmtpDto>>, BadRequest>> GetSmtpConfigurationAsync(CancellationToken cancellationToken);
    Task<Results<Ok<SettingSmtpDto>, BadRequest>> CreateSmtpConfigurationAsync(CreateSettingSmtpDto request, CancellationToken cancellationToken);
    Task<Results<Ok<SettingSmtpDto>, BadRequest>> EditSmtpConfigurationAsync(EditSettingSmtpDto request, CancellationToken cancellationToken);
}
