using AutoMapper;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using InvioEmailSvc.BusinessLayer.Mediator.Command;
using InvioEmailSvc.DataAccessLayer;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.Extensions.Logging;

namespace InvioEmailSvc.BusinessLayer.Mediator.Handlers;

public class CreateEmailMessageHandler(AppDbContext dbContext, IMapper mapper, ILogger<CreateEmailMessageHandler> logger)
    : ICommandHandler<CreateEmailMessageCommand, bool>
{
    public async Task<bool> Handle(CreateEmailMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Creazione del messaggio email
            var emailMessage = mapper.Map<EmailMessage>(request);

            dbContext.EmailMessages.Add(emailMessage);

            //Creazione del messaggio email nella coda di invio
            var emailOutboxMessage = new EmailOutboxMessage
            {
                EmailMessageId = emailMessage.Id,
                IsSent = false,
                CreatedByUserId = Guid.Empty,
                CreatedDateTime = DateTime.UtcNow
            };

            dbContext.EmailOutboxMessages.Add(emailOutboxMessage);

            //Salvataggio delle modifiche
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to create email message");
            return false;
        }
    }
}
