using MailKit.Security;

namespace ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;

public record CreateSettingSmtpDto(string Host, int Port, SecureSocketOptions Security, string Username, string Password);
