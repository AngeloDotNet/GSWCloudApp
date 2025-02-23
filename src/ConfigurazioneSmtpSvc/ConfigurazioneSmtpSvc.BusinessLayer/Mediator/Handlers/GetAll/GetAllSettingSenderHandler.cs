using ConfigurazioneSmtpSvc.BusinessLayer.Mapper;
using ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Query;
using ConfigurazioneSmtpSvc.DataAccessLayer;
using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using ConfigurazioneSmtpSvc.Shared.DTO.SettingSender;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;

namespace ConfigurazioneSmtpSvc.BusinessLayer.Mediator.Handlers.GetAll;

public class GetAllSettingSenderHandler(IGenericService genericService, AppDbContext dbContext) : IQueryHandler<GetAllSettingSenderQuery, List<SettingSenderDto>>
{
    public async Task<List<SettingSenderDto>> Handle(GetAllSettingSenderQuery request, CancellationToken cancellationToken)
    {
        var entity = await genericService.GetAllAsync<SettingSender>(dbContext, null!, null!, null!, true);

        return entity.Select(x => x.ToSettingSenderDto()).ToList();
    }
}