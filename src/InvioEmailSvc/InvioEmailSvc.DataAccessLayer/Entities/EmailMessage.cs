using GSWCloudApp.Common.Identity.Entities;

namespace InvioEmailSvc.DataAccessLayer.Entities;

public class EmailMessage : SoftDeletableEntity<Guid>
{
    //public string? SenderName { get; set; }
    //public string? SenderEmail { get; set; }
    public IList<string> To { get; set; } = [];
    public IList<string> Cc { get; set; } = [];
    public IList<string> Bcc { get; set; } = [];
    public IList<string> ReplyTo { get; set; } = [];
    public string? Subject { get; set; }
    //public string? HtmlContent { get; set; }
    public string? TextContent { get; set; }
}