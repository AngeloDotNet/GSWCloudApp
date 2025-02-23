using ConfigurazioneSmtpSvc.BusinessLayer.Mapper;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Command.Edit;
using ConfigurazioneSmtpSvc.DataAccessLayer;
using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using GSWCloudApp.Common.Helpers;
using GSWCloudApp.Common.Mediator.Interfaces.Command;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Handlers.Edit;

public class EditSettingSmtpHandler(IGenericService genericService, AppDbContext dbContext,
    IHttpContextAccessor httpContextAccessor) : ICommandHandler<EditSettingSmtpCommand, SettingSmtpDto>
{
    public async Task<SettingSmtpDto> Handle(EditSettingSmtpCommand request, CancellationToken cancellationToken)
    {
        var entity = await genericService.GetByIdAsync<SettingSmtp>(request.Id, dbContext)
            ?? throw new Exception("Entity not found");

        var userId = UsersHelpers.GetUserId(httpContextAccessor);
        var updateEntity = request.ToSettingSmtp();

        updateEntity.CreatedByUserId = entity.CreatedByUserId;
        updateEntity.CreatedDateTime = entity.CreatedDateTime;

        updateEntity.ModifiedByUserId = userId;
        updateEntity.ModifiedDateTime = DateTime.UtcNow;

        var response = await genericService.UpdateAsync(updateEntity, dbContext);
        return response.ToSettingSmtpDto();
    }
}