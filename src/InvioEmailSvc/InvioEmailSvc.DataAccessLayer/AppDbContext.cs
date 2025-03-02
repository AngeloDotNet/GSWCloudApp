using System.Reflection;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvioEmailSvc.DataAccessLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<EmailMessage> EmailMessages { get; set; } = null!;
    public DbSet<EmailOutboxMessage> EmailOutboxMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}