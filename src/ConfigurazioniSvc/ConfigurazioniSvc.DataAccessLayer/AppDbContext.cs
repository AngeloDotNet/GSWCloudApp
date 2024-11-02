using System.Security.Claims;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConfigurazioniSvc.DataAccessLayer;

public class AppDbContext : DbContext
{
    private readonly IConfiguration configuration;
    private readonly HttpContext? httpContext;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        this.configuration = configuration;

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

    public DbSet<Configurazione> Configurazioni { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var migrationsAssembly = GetType().Assembly
            .GetName().Name!
            .Replace(".DataAccessLayer", ".Migrations");

        var connectionString = configuration.GetConnectionString("SQLConnection");

        optionsBuilder.UseNpgsql(connectionString, options =>
        {
            options.MigrationsAssembly(migrationsAssembly);
            options.MigrationsHistoryTable("StoricoMigrazioni");
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
        });

        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}