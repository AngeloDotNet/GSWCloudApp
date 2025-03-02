using InvioEmailSvc.DataAccessLayer;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvioEmailSvc.BusinessLayer.Extensions;

public static class WorkerExtensions
{
    public static async Task<List<EmailMessage>> GetListEmailMessageAsync(this AppDbContext dbContext, CancellationToken cancellationToken)
    {
        var listEmails = new List<EmailMessage>();
        var listUnsentEmail = new List<EmailOutboxMessage>();

        listUnsentEmail = await dbContext.EmailOutboxMessages.Where(x => !x.IsSent).ToListAsync(cancellationToken);

        foreach (var email in listUnsentEmail)
        {
            var emailMessage = new EmailMessage();

            emailMessage = await dbContext.GetEmailMessageAsync(email.EmailMessageId, cancellationToken);

            listEmails.Add(emailMessage!);
        }

        return listEmails;
    }

    public static async Task<EmailMessage?> GetEmailMessageAsync(this AppDbContext dbContext, Guid emailMessageId, CancellationToken cancellationToken)
        => await dbContext.EmailMessages.FirstOrDefaultAsync(x => x.Id == emailMessageId, cancellationToken);

    public static async Task UpdateEmailAsync(this AppDbContext dbContext, Guid messageId, CancellationToken cancellationToken)
    {
        var emailOutboxMessage = new EmailOutboxMessage
        {
            Id = messageId,
            IsSent = true,
            ModifiedByUserId = Guid.Empty,
            ModifiedDateTime = DateTime.UtcNow
        };

        var emailmessage = new EmailMessage
        {
            Id = messageId,
            ModifiedByUserId = Guid.Empty,
            ModifiedDateTime = DateTime.UtcNow
        };

        dbContext.EmailOutboxMessages.Update(emailOutboxMessage);
        dbContext.EmailMessages.Update(emailmessage);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}