using ConfigurazioneSmtpSvc.BusinessLayer.Mapper;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;
using ConfigurazioneSmtpSvc.DataAccessLayer;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Handlers.Create;

public class CreateSettingSenderHandler(IGenericService genericService, AppDbContext dbContext,
    IHttpContextAccessor httpContextAccessor) : ICommandHandler<CreateSettingSenderCommand, SettingSenderDto>
{
    public async Task<SettingSenderDto> Handle(CreateSettingSenderCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToSettingSender();
        var userId = UsersHelpers.GetUserId(httpContextAccessor);

        entity.CreatedByUserId = userId;
        entity.CreatedDateTime = DateTime.UtcNow;

        var response = await genericService.PostAsync(entity, dbContext);
        return response.ToSettingSenderDto();
    }
}