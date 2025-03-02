using GSWCloudApp.Common.Identity.Entities;

namespace InvioEmailSvc.DataAccessLayer.Entities;

public class EmailOutboxMessage : SoftDeletableEntity<Guid>
{
    public Guid EmailMessageId { get; set; }
    public bool IsSent { get; set; }
}