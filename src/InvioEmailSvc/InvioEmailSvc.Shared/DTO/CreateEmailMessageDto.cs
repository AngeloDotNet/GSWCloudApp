namespace InvioEmailSvc.Shared.DTO;

public record CreateEmailMessageDto(
    IList<string> To,
    IList<string> Cc,
    IList<string> Bcc,
    IList<string> ReplyTo,
    string Subject,
    string TextContent
);