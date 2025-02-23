using System.Reflection;
using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConfigurazioneSmtpSvc.DataAccessLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<SettingSender> SettingsSender { get; set; } = null!;
    public DbSet<SettingSmtp> SettingsSmtp { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}