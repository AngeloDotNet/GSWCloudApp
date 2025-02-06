using System.Security.Claims;
using GSWCloudApp.Common.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GSWCloudApp.Common.Identity;

public abstract class BaseDbContext : DbContext
{
    protected readonly IHttpContextAccessor? contextAccessor;

    protected BaseDbContext(DbContextOptions options, IHttpContextAccessor? contextAccessor) : base(options)
    {
        this.contextAccessor = contextAccessor;

        SavingChanges += (_, _) =>
        {
            OnSavingChangesHandler(ChangeTracker, contextAccessor?.HttpContext);
        };
    }

    private static void OnSavingChangesHandler(ChangeTracker changeTracker, HttpContext? httpContext)
    {
        var now = DateTimeOffset.UtcNow;

        var guid = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid? userId = guid != null ? Guid.Parse(guid) : null;

        foreach (var entry in changeTracker.Entries())
        {
            switch (entry.Entity)
            {
                case ITrackableEntity trackableEntity when entry.State == EntityState.Added:
                    {
                        trackableEntity.CreatedByUserId = userId ?? Guid.Empty;
                        trackableEntity.CreatedDateTime = DateTime.UtcNow;

                        break;
                    }

                case ITrackableEntity trackableEntity when entry.State == EntityState.Modified:
                    {
                        if (entry.Properties.FirstOrDefault(x => x is
                            {
                                IsModified: true,
                                Metadata.Name: nameof(ITrackableEntity.CreatedByUserId) or nameof(ITrackableEntity.CreatedDateTime)
                            }) != null)

                            throw new DbUpdateException($"Attempt to change created audit trails on a modified {entry.Entity.GetType().FullName}");

                        trackableEntity.ModifiedByUserId = userId;
                        trackableEntity.ModifiedDateTime = DateTime.UtcNow;

                        break;
                    }

                case ISoftDeletableEntity softDeletableEntity when entry.State == EntityState.Deleted:
                    {
                        entry.State = EntityState.Modified;

                        softDeletableEntity.DeletedByUserId = userId;
                        softDeletableEntity.DeletedDateTime = DateTime.UtcNow;

                        break;
                    }
            }
        }
    }
}
