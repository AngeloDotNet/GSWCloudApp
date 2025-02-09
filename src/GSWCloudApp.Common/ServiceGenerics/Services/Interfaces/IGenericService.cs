using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;

public interface IGenericService
{
    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> from the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="dbContext">The database context to use.</param>
    /// <param name="includes">A function to include related entities.</param>
    /// <param name="filter">An expression to filter the entities.</param>
    /// <param name="orderBy">An expression to order the entities.</param>
    /// <param name="ascending">A boolean indicating whether the order should be ascending.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IQueryable{TEntity}"/> of entities.</returns>
    Task<IQueryable<TEntity>> GetAllAsync<TEntity>(DbContext dbContext, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null!,
        Expression<Func<TEntity, bool>> filter = null!, Expression<Func<TEntity, object>> orderBy = null!, bool ascending = true) where TEntity : class;

    /// <summary>
    /// Retrieves an entity of type <typeparamref name="TEntity"/> by its identifier from the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    Task<TEntity> GetByIdAsync<TEntity>(Guid id, DbContext dbContext) where TEntity : class;

    /// <summary>
    /// Adds a new entity of type <typeparamref name="TEntity"/> to the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to add.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<TEntity> PostAsync<TEntity>(TEntity entity, DbContext dbContext) where TEntity : class;

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="TEntity"/> in the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to update.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    Task<TEntity> UpdateAsync<TEntity>(TEntity entity, DbContext dbContext) where TEntity : class;

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> by its identifier from the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync<TEntity>(Guid id, DbContext dbContext) where TEntity : class;

    /// <summary>
    /// Retrieves a paginated list of entities of type <typeparamref name="TEntity"/> from the specified <paramref name="query"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="query">The query to retrieve the entities.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Paging{TEntity}"/> object with the paginated entities.</returns>
    Task<Paging<TEntity>> GetAllPaginingAsync<TEntity>(IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : class;
}
