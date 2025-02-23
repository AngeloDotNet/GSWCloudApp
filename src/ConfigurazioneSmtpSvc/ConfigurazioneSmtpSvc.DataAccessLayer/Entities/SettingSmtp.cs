using GSWCloudApp.Common.Identity.Entities;
using MailKit.Security;

namespace ConfigurazioneSmtpSvc.DataAccessLayer.Entities;

public class SettingSmtp : SoftDeletableEntity<Guid>
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public SecureSocketOptions Security { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}