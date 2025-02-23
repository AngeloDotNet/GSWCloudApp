using ConfigurazioneSmtpSvc.BusinessLayer.Mapper;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Create;
using ConfigurazioneSmtpSvc.DataAccessLayer;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Handlers.Create;

public class CreateSettingSmtpHandler(IGenericService genericService, AppDbContext dbContext,
    IHttpContextAccessor httpContextAccessor) : ICommandHandler<CreateSettingSmtpCommand, SettingSmtpDto>
{
    public async Task<SettingSmtpDto> Handle(CreateSettingSmtpCommand request, CancellationToken cancellationToken)
    {
        //var entity = mapper.Map<SettingSmtp>(request);

        var entity = request.ToSettingSmtp();
        var userId = UsersHelpers.GetUserId(httpContextAccessor);

        entity.CreatedByUserId = userId;
        entity.CreatedDateTime = DateTime.UtcNow;

        var response = await genericService.PostAsync(entity, dbContext);
        return response.ToSettingSmtpDto();

        //return mapper.Map<SettingSmtpDto>(response);
    }
}
