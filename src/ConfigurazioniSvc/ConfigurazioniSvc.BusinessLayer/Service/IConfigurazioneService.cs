using AutoMapper;
using ConfigurazioniSvc.DataAccessLayer;
using ConfigurazioniSvc.Shared.DTO;
using GSWCloudApp.Common.RedisCache;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurazioniSvc.BusinessLayer.Service;

public interface IConfigurazioneService
{
    Task<Results<Ok<List<ConfigurazioneDto>>, NotFound>> GetAllAsync([FromQuery] bool cacheData, AppDbContext dbContext, ICacheService cacheService, IMapper mapper);
    Task<Results<Ok<ConfigurazioneDto>, NotFound>> GetByIdAsync(Guid id, AppDbContext dbContext, ICacheService cacheService, IMapper mapper);
    Task<Results<Ok<ConfigurazioneDto>, BadRequest<string>>> PostAsync(CreateConfigurazioneDto configurazioneDto, AppDbContext dbContext, IMapper mapper);
    Task<Results<Ok<ConfigurazioneDto>, NotFound, BadRequest<string>>> UpdateAsync(Guid id, EditConfigurazioneDto configurazioneDto, AppDbContext dbContext, IMapper mapper);
    Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, AppDbContext dbContext);
    Task<Results<Ok<List<ConfigurazioneDto>>, NotFound>> FilterAsync(Guid festaId, AppDbContext dbContext, ICacheService cacheService, IMapper mapper);
}
