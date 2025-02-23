using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using MailKit.Security;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;

public class CreateSettingSmtpCommand(CreateSettingSmtpDto dto) : ICommand<SettingSmtpDto>
{
    public string Host { get; set; } = dto.Host;
    public int Port { get; set; } = dto.Port;
    public SecureSocketOptions Security { get; set; } = dto.Security;
    public string Username { get; set; } = dto.Username;
    public string Password { get; set; } = dto.Password;
}