using System.Reflection;
using System.Security.Claims;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ConfigurazioniSvc.DataAccessLayer;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        var httpContext = httpContextAccessor.HttpContext;
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

    public DbSet<Configurazione> Configurazioni { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
