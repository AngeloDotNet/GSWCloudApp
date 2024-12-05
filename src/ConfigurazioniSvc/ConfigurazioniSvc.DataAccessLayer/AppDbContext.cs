using System.Reflection;
using System.Security.Claims;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ConfigurazioniSvc.DataAccessLayer;

/// <summary>
/// Represents the application's database context.
/// </summary>
public class AppDbContext : DbContext
{
    private readonly HttpContext? httpContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
    /// <param name="httpContextAccessor">The accessor to get the current HTTP context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        this.httpContext = httpContextAccessor.HttpContext;
        var guid = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid? userId = guid != null ? Guid.Parse(guid) : null;

        SavingChanges += (sender, args) =>
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not Configurazione entity)
                {
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedByUserId = userId ?? Guid.Empty;
                        entity.CreatedDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entity.ModifiedByUserId = userId;
                        entity.ModifiedDateTime = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;

                        entity.DeletedByUserId = userId;
                        entity.DeletedDateTime = DateTime.UtcNow;
                        break;
                }
            }
        };
    }

    /// <summary>
    /// Gets or sets the Configurazioni DbSet.
    /// </summary>
    public DbSet<Configurazione> Configurazioni { get; set; } = null!;

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
