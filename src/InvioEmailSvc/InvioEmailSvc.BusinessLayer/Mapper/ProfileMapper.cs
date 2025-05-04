using InvioEmailSvc.BusinessLayer.Mediator.Command;
using InvioEmailSvc.DataAccessLayer.Entities;

namespace InvioEmailSvc.BusinessLayer.Mapper;

public static class ProfileMapper
{
    public static EmailMessage CreateEmailMessageCommandToEntity(this CreateEmailMessageCommand command)
    {
        return new EmailMessage
        {
            To = command.To,
            Cc = command.Cc,
            Bcc = command.Bcc,
            ReplyTo = command.ReplyTo,
            Subject = command.Subject,
            TextContent = command.TextContent
        };
    }
}