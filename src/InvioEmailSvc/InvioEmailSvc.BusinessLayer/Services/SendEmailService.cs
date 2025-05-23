using GSWCloudApp.Common.Mediator.Interfaces.Command;
using InvioEmailSvc.BusinessLayer.Mediator.Command;
using InvioEmailSvc.BusinessLayer.Services.Interfaces;
using InvioEmailSvc.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InvioEmailSvc.BusinessLayer.Services;

public class SendEmailService(ICommandHandler<CreateEmailMessageCommand, bool> handler) : ISendEmailService
{
    public async Task<Results<Ok, BadRequest>> SendEmailAsync(CreateEmailMessageDto request, CancellationToken cancellationToken)
    {
        return await handler.Handle(new CreateEmailMessageCommand(request), cancellationToken) switch
        {
            true => TypedResults.Ok(),
            _ => TypedResults.BadRequest()
        };
    }
}