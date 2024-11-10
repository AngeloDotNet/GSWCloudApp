using ConfigurazioniSvc.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConfigurazioniSvc.Helpers;

public static class ApplicationHelpers
{
    public static async Task ConfigureDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!dbContext.Database.IsInMemory())
        {
            await EnsureDatabaseAsync();
            await RunMigrationsAsync();
        }
        else
        {
            dbContext.Database.EnsureCreated();
        }

        async Task EnsureDatabaseAsync()
        {
            var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                if (!await dbCreator.ExistsAsync())
                {
                    await dbCreator.CreateAsync();
                }
            });
        }

        async Task RunMigrationsAsync()
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync();

                await dbContext.Database.MigrateAsync();
                await transaction.CommitAsync();
            });
        }
    }
}