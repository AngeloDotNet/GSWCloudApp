using GSWCloudApp.Common.Identity.Entities;

namespace ConfigurazioneSmtpSvc.DataAccessLayer.Entities;

public class SettingSender : SoftDeletableEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
