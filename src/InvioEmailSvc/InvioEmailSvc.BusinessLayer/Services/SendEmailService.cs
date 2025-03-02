using InvioEmailSvc.BusinessLayer.Mediator.Command;
using InvioEmailSvc.BusinessLayer.Services.Interfaces;
using InvioEmailSvc.Shared.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InvioEmailSvc.BusinessLayer.Services;

public class SendEmailService : ISendEmailService
{
    public async Task<Results<Ok, BadRequest>> SendEmailAsync(CreateEmailMessageDto request, ISender sender, CancellationToken cancellationToken = default)
    {
        return await sender.Send(new CreateEmailMessageCommand(request), cancellationToken) switch
        {
            true => TypedResults.Ok(),
            _ => TypedResults.BadRequest()
        };
    }
}