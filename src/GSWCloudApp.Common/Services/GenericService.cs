using AutoMapper;
using GSWCloudApp.Common.RedisCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSWCloudApp.Common.Services;

public class GenericService : IGenericService
{
    public async Task<Results<Ok<List<TDto>>, NotFound>> GetAllAsync<TEntity, TDto>([FromQuery] bool cacheData, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class
    {
        var cacheKey = typeof(TEntity).Name;
        var entity = new List<TEntity>();

        if (!cacheData)
        {
            entity = await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

            if (entity.Count == 0)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<List<TDto>>(entity));
        }

        entity = await cacheService.GetCacheAsync<List<TEntity>>(cacheKey);

        if (entity != null)
        {
            return TypedResults.Ok(mapper.Map<List<TDto>>(entity));
        }

        entity = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();

        if (entity.Count == 0)
        {
            return TypedResults.NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity);

        var result = mapper.Map<List<TDto>>(entity);

        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<TDto>, NotFound>> GetByIdAsync<TEntity, TDto>(Guid id, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class
    {
        var cacheKey = $"{typeof(TEntity).Name}-{id}";
        var entity = await cacheService.GetCacheAsync<TEntity>(cacheKey);

        if (entity != null)
        {
            return TypedResults.Ok(mapper.Map<TDto>(entity));
        }

        entity = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity);

        var result = mapper.Map<TDto>(entity);

        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<TDto>, BadRequest<string>>> PostAsync<TEntity, TDto, TCreateDto>(TCreateDto createDto, DbContext dbContext, IMapper mapper)
        where TEntity : class
        where TDto : class
    {
        var entity = mapper.Map<TEntity>(createDto);

        dbContext.Set<TEntity>().Add(entity);

        try
        {
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok(mapper.Map<TDto>(entity));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<Results<Ok<TDto>, NotFound, BadRequest<string>>> UpdateAsync<TEntity, TDto, TEditDto>(Guid id, TEditDto editDto, DbContext dbContext, IMapper mapper)
        where TEntity : class
        where TDto : class
    {
        var entity = await dbContext.Set<TEntity>().FindAsync(id);

        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        mapper.Map(editDto, entity);

        dbContext.Set<TEntity>().Update(entity);

        try
        {
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok(mapper.Map<TDto>(entity));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<Results<NoContent, NotFound>> DeleteAsync<TEntity>(Guid id, DbContext dbContext)
        where TEntity : class
    {
        var entity = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public async Task<Results<Ok<List<TDto>>, NotFound>> FilterByIdFestaAsync<TEntity, TDto>(Guid festaId, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class
    {
        var cacheKey = $"{typeof(TEntity).Name}-{festaId}";
        var entity = await cacheService.GetCacheAsync<List<TEntity>>(cacheKey);

        if (entity != null)
        {
            return TypedResults.Ok(mapper.Map<List<TDto>>(entity));
        }

        entity = await dbContext.Set<TEntity>()
            .AsNoTracking()
            .Where(e => EF.Property<Guid>(e, "FestaId") == festaId)
            .ToListAsync();

        if (entity.Count == 0)
        {
            return TypedResults.NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity);

        return TypedResults.Ok(mapper.Map<List<TDto>>(entity));
    }
}