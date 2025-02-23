using ConfigurazioneSmtpSvc.BusinessLayer.Mapper;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Query;
using ConfigurazioneSmtpSvc.DataAccessLayer;
using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSmtp;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Handlers.GetAll;

public class GetAllSettingSmtpHandler(IGenericService genericService, AppDbContext dbContext) : IQueryHandler<GetAllSettingSmtpQuery, List<SettingSmtpDto>>
{
    public async Task<List<SettingSmtpDto>> Handle(GetAllSettingSmtpQuery request, CancellationToken cancellationToken)
    {
        var entity = await genericService.GetAllAsync<SettingSmtp>(dbContext, null!, null!, null!, true);

        return entity.Select(x => x.ToSettingSmtpDto()).ToList();
    }
}