using AutoMapper;
using GSWCloudApp.Common.RedisCache.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSWCloudApp.Common.ServiceGenerics.Services;

/// <summary>
/// Provides generic service methods for handling CRUD operations.
/// </summary>
internal class GenericService : IGenericService
{
    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> and maps them to <typeparamref name="TDto"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <param name="cacheData">Indicates whether to cache the data.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="cacheService">The cache service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>A list of DTOs or a NotFound result.</returns>
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

    /// <summary>
    /// Retrieves an entity of type <typeparamref name="TEntity"/> by its ID and maps it to <typeparamref name="TDto"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="cacheService">The cache service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>The DTO or a NotFound result.</returns>
    public async Task<Results<Ok<TDto>, NotFound>> GetByIdAsync<TEntity, TDto>([FromQuery] bool cacheData, Guid id, DbContext dbContext, ICacheService cacheService, IMapper mapper)
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

    /// <summary>
    /// Creates a new entity of type <typeparamref name="TEntity"/> from the provided DTO and maps it to <typeparamref name="TDto"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TCreateDto">The type of the create DTO.</typeparam>
    /// <param name="createDto">The create DTO.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>The created DTO or a BadRequest result.</returns>
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

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="TEntity"/> with the provided DTO and maps it to <typeparamref name="TDto"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <typeparam name="TEditDto">The type of the edit DTO.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="editDto">The edit DTO.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>The updated DTO, a NotFound result, or a BadRequest result.</returns>
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

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> by its ID.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="dbContext">The database context.</param>
    /// <returns>A NoContent result or a NotFound result.</returns>
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

    /// <summary>
    /// Filters entities of type <typeparamref name="TEntity"/> by the specified festa ID and maps them to <typeparamref name="TDto"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDto">The type of the DTO.</typeparam>
    /// <param name="festaId">The festa ID.</param>
    /// <param name="dbContext">The database context.</param>
    /// <param name="cacheService">The cache service.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns>A list of DTOs or a NotFound result.</returns>
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
