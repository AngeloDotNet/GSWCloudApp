using GSWCloudApp.Common.Mediator.Interfaces.Command;
using InvioEmailSvc.Shared.DTO;

namespace InvioEmailSvc.BusinessLayer.Mediator.Command;

public record CreateEmailMessageCommand(IList<string> To, IList<string> Cc, IList<string> Bcc, IList<string> ReplyTo,
    string Subject, string TextContent) : ICommand<bool>
{
    public CreateEmailMessageCommand(CreateEmailMessageDto dto) : this(dto.To, dto.Cc, dto.Bcc, dto.ReplyTo,
        dto.Subject, dto.TextContent)
    { }
}