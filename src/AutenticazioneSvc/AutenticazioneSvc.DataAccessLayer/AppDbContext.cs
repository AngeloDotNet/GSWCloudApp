using System.Reflection;
using GSWCloudApp.Common.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutenticazioneSvc.DataAccessLayer;

public class AppDbContext(DbContextOptions<AppDbContext> options) : SecurityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}