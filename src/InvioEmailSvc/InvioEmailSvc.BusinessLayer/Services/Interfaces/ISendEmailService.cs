using InvioEmailSvc.Shared.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InvioEmailSvc.BusinessLayer.Services.Interfaces;

public interface ISendEmailService
{
    Task<Results<Ok, BadRequest>> SendEmailAsync(CreateEmailMessageDto request, CancellationToken cancellationToken);
}