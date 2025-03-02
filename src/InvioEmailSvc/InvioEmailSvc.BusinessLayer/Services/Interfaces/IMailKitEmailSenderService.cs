using InvioEmailSvc.DataAccessLayer.Entities;

namespace InvioEmailSvc.BusinessLayer.Services.Interfaces;

public interface IMailKitEmailSenderService
{
    Task<bool> SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}
