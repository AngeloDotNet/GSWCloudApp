using AutoMapper;
using GSWCloudApp.Common.RedisCache.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSWCloudApp.Common.ServiceGenerics.Services;

/// <summary>
/// Defines generic service methods for handling CRUD operations.
/// </summary>
internal interface IGenericService
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
    Task<Results<Ok<List<TDto>>, NotFound>> GetAllAsync<TEntity, TDto>([FromQuery] bool cacheData, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class;

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
    Task<Results<Ok<TDto>, NotFound>> GetByIdAsync<TEntity, TDto>([FromQuery] bool cacheData, Guid id, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class;

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
    Task<Results<Ok<TDto>, BadRequest<string>>> PostAsync<TEntity, TDto, TCreateDto>(TCreateDto createDto, DbContext dbContext, IMapper mapper)
        where TEntity : class
        where TDto : class;

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
    Task<Results<Ok<TDto>, NotFound, BadRequest<string>>> UpdateAsync<TEntity, TDto, TEditDto>(Guid id, TEditDto editDto, DbContext dbContext, IMapper mapper)
        where TEntity : class
        where TDto : class;

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> by its ID.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="dbContext">The database context.</param>
    /// <returns>A NoContent result or a NotFound result.</returns>
    Task<Results<NoContent, NotFound>> DeleteAsync<TEntity>(Guid id, DbContext dbContext)
        where TEntity : class;

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
    Task<Results<Ok<List<TDto>>, NotFound>> FilterByIdFestaAsync<TEntity, TDto>(Guid festaId, DbContext dbContext, ICacheService cacheService, IMapper mapper)
        where TEntity : class
        where TDto : class;
}
