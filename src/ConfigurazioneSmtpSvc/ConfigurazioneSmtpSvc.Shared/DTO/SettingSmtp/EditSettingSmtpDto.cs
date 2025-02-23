using MailKit.Security;

namespace ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;

public record EditSettingSmtpDto(Guid Id, string Host, int Port, SecureSocketOptions Security, string Username, string Password);
