using InvioEmailSvc.Shared.DTO;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InvioEmailSvc.BusinessLayer.Services.Interfaces;

public interface ISendEmailService
{
    Task<Results<Ok, BadRequest>> SendEmailAsync(CreateEmailMessageDto emailMessageDto, ISender sender, CancellationToken cancellationToken = default);
}