using AutoMapper;
using ConfigurazioniSvc.DataAccessLayer;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using ConfigurazioniSvc.Shared.DTO;
using GSWCloudApp.Common.RedisCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfigurazioniSvc.BusinessLayer.Service;

public class ConfigurazioneService : IConfigurazioneService
{
    public async Task<Results<Ok<List<ConfigurazioneDto>>, NotFound>> GetAllAsync([FromQuery] bool cacheData, AppDbContext dbContext, ICacheService cacheService, IMapper mapper)
    {
        var cacheKey = "ConfigurazioniSvc";
        var entity = new List<Configurazione>();

        if (!cacheData)
        {
            entity = await dbContext.Configurazioni.AsNoTracking().ToListAsync();

            if (entity.Count == 0)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<List<ConfigurazioneDto>>(entity));
        }

        entity = await cacheService.GetCacheAsync<List<Configurazione>>(cacheKey);

        if (entity != null)
        {
            return TypedResults.Ok(mapper.Map<List<ConfigurazioneDto>>(entity));
        }

        entity = await dbContext.Configurazioni
            .AsNoTracking()
            .ToListAsync();

        if (entity.Count == 0)
        {
            return TypedResults.NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity);

        var result = mapper.Map<List<ConfigurazioneDto>>(entity);

        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<ConfigurazioneDto>, NotFound>> GetByIdAsync(Guid id, AppDbContext dbContext, ICacheService cacheService, IMapper mapper)
    {
        var cacheKey = $"ConfigurazioneSvc-{id}";
        var entity = new Configurazione();

        entity = await cacheService.GetCacheAsync<Configurazione>(cacheKey);

        if (entity != null)
        {
            return TypedResults.Ok(mapper.Map<ConfigurazioneDto>(entity));
        }

        entity = await dbContext.Configurazioni
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity);

        var result = mapper.Map<ConfigurazioneDto>(entity);

        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<ConfigurazioneDto>, BadRequest<string>>> PostAsync(CreateConfigurazioneDto configurazioneDto, AppDbContext dbContext, IMapper mapper)
    {
        var entity = mapper.Map<Configurazione>(configurazioneDto);

        dbContext.Configurazioni.Add(entity);

        try
        {
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok(mapper.Map<ConfigurazioneDto>(entity));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<Results<Ok<ConfigurazioneDto>, NotFound, BadRequest<string>>> UpdateAsync(Guid id, EditConfigurazioneDto configurazioneDto, AppDbContext dbContext, IMapper mapper)
    {
        var entity = await dbContext.Configurazioni.FindAsync(id);

        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        mapper.Map(configurazioneDto, entity);

        dbContext.Configurazioni.Update(entity);

        try
        {
            await dbContext.SaveChangesAsync();

            return TypedResults.Ok(mapper.Map<ConfigurazioneDto>(entity));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    public async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, AppDbContext dbContext)
    {
        var result = await dbContext.Configurazioni
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            return TypedResults.NotFound();
        }

        dbContext.Configurazioni.Remove(result);
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public async Task<Results<Ok<List<ConfigurazioneDto>>, NotFound>> FilterAsync(Guid festaId, AppDbContext dbContext, ICacheService cacheService, IMapper mapper)
    {
        var cacheKey = $"ConfigurazioniSvc-{festaId}";
        var entity = new List<Configurazione>();

        entity = await cacheService.GetCacheAsync<List<Configurazione>>(cacheKey);

        if (entity != null)
        {
            return TypedResults.Ok(mapper.Map<List<ConfigurazioneDto>>(entity));
        }

        entity = await dbContext.Configurazioni
            .AsNoTracking()
            .Where(x => x.FestaId == festaId)
            .ToListAsync();

        if (entity.Count == 0)
        {
            return TypedResults.NotFound();
        }

        await cacheService.SetCacheAsync(cacheKey, entity);

        return TypedResults.Ok(mapper.Map<List<ConfigurazioneDto>>(entity));
    }
}
