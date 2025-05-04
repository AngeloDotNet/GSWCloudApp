using GSWCloudApp.Common.Mediator.Interfaces.Command;
using InvioEmailSvc.BusinessLayer.Mapper;
using InvioEmailSvc.BusinessLayer.Mediator.Command;
using InvioEmailSvc.DataAccessLayer;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.Extensions.Logging;

namespace InvioEmailSvc.BusinessLayer.Mediator.Handlers;

//TODO: Code cleanup
//public class CreateEmailMessageHandler(AppDbContext dbContext, IMapper mapper, ILogger<CreateEmailMessageHandler> logger)
//    : ICommandHandler<CreateEmailMessageCommand, bool>
public class CreateEmailMessageHandler(AppDbContext dbContext, ILogger<CreateEmailMessageHandler> logger)
    : ICommandHandler<CreateEmailMessageCommand, bool>
{
    public async Task<bool> Handle(CreateEmailMessageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //TODO: Code cleanup
            //var emailMessage = mapper.Map<EmailMessage>(request);
            var emailMessage = ProfileMapper.CreateEmailMessageCommandToEntity(request);
            dbContext.EmailMessages.Add(emailMessage);

            var emailOutboxMessage = new EmailOutboxMessage
            {
                EmailMessageId = emailMessage.Id,
                IsSent = false,
                CreatedByUserId = Guid.Empty,
                CreatedDateTime = DateTime.UtcNow
            };

            dbContext.EmailOutboxMessages.Add(emailOutboxMessage);
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
