using System.Linq.Expressions;
using GSWCloudApp.Common.ServiceGenerics.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GSWCloudApp.Common.ServiceGenerics.Services;

public class GenericService : IGenericService
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
    public async Task<IQueryable<TEntity>> GetAllAsync<TEntity>(DbContext dbContext, Func<IQueryable<TEntity>,
        IIncludableQueryable<TEntity, object>> includes = null!, Expression<Func<TEntity, bool>> filter = null!,
        Expression<Func<TEntity, object>> orderBy = null!, bool ascending = true) where TEntity : class
    {
        var query = dbContext.Set<TEntity>().AsNoTracking();

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        }

        return await Task.FromResult(query);
    }

    /// <summary>
    /// Retrieves an entity of type <typeparamref name="TEntity"/> by its identifier from the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    public async Task<TEntity> GetByIdAsync<TEntity>(Guid id, DbContext dbContext) where TEntity : class
    {
        var entity = await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

        if (entity is null)
        {
            return null!;
        }

        return entity;
    }

    /// <summary>
    /// Adds a new entity of type <typeparamref name="TEntity"/> to the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to add.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    public async Task<TEntity> PostAsync<TEntity>(TEntity entity, DbContext dbContext) where TEntity : class
    {
        dbContext.Set<TEntity>().Add(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="TEntity"/> in the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to update.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, DbContext dbContext) where TEntity : class
    {
        dbContext.Set<TEntity>().Update(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Deletes an entity of type <typeparamref name="TEntity"/> by its identifier from the specified <paramref name="dbContext"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="dbContext">The database context to use.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteAsync<TEntity>(Guid id, DbContext dbContext) where TEntity : class
    {
        var entity = await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);

        dbContext.Set<TEntity>().Remove(entity!);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves a paginated list of entities of type <typeparamref name="TEntity"/> from the specified <paramref name="query"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="query">The query to retrieve the entities.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of the page to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Paging{TEntity}"/> object with the paginated entities.</returns>
    public async Task<Paging<TEntity>> GetAllPaginingAsync<TEntity>(IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : class
    {
        var entities = new Paging<TEntity>
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalItems = await query.CountAsync(),
            Items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
        };

        return entities.Items.Count switch
        {
            0 => null!,
            _ => entities
        };
    }
}
