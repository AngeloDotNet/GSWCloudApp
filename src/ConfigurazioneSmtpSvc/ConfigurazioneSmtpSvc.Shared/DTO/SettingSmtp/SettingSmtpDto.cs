using MailKit.Security;

namespace ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;

//public class SettingSmtpDto
//{
//    public Guid Id { get; set; }
//    public string? Host { get; set; }
//    public int Port { get; set; }
//    public SecureSocketOptions Security { get; set; }
//    public string? Username { get; set; }
//    public string? Password { get; set; }
//}

public record SettingSmtpDto(Guid Id, string Host, int Port, SecureSocketOptions Security, string Username, string Password);
