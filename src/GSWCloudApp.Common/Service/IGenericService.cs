using AutoMapper;
using GSWCloudApp.Common.RedisCache;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSWCloudApp.Common.Service;

public interface IGenericService
{
    Task<Results<Ok<List<TDto>>, NotFound>> GetAllAsync<TEntity, TDto>([FromQuery] bool cacheData, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class;

    Task<Results<Ok<TDto>, NotFound>> GetByIdAsync<TEntity, TDto>(Guid id, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class;

    Task<Results<Ok<TDto>, BadRequest<string>>> PostAsync<TEntity, TDto, TCreateDto>(TCreateDto createDto, DbContext dbContext, IMapper mapper)
        where TEntity : class
        where TDto : class;

    Task<Results<Ok<TDto>, NotFound, BadRequest<string>>> UpdateAsync<TEntity, TDto, TEditDto>(Guid id, TEditDto editDto, DbContext dbContext, IMapper mapper)
        where TEntity : class
        where TDto : class;

    Task<Results<NoContent, NotFound>> DeleteAsync<TEntity>(Guid id, DbContext dbContext)
        where TEntity : class;

    Task<Results<Ok<List<TDto>>, NotFound>> FilterAsync<TEntity, TDto>(Guid festaId, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class;
}