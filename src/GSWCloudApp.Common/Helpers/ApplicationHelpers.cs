using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Helpers;

public static class ApplicationHelpers
{
    public static async void ApplyMigrations<TDbContext>(this WebApplication app) where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        if (!await dbCreator.ExistsAsync())
        {
            await dbCreator.CreateAsync();
        }

        dbContext.Database.Migrate();
    }

    //TODO: da eliminare al prossimo refactoring
    //public static async Task ConfigureDatabaseAsync<TDbContext>(IServiceProvider serviceProvider) where TDbContext : DbContext
    //{
    //    using var scope = serviceProvider.CreateScope();
    //    var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

    //    if (!dbContext.Database.IsInMemory())
    //    {
    //        await EnsureDatabaseAsync();
    //        await RunMigrationsAsync();
    //    }
    //    else
    //    {
    //        dbContext.Database.EnsureCreated();
    //    }

    //    async Task EnsureDatabaseAsync()
    //    {
    //        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

    //        if (!await dbCreator.ExistsAsync())
    //        {
    //            await dbCreator.CreateAsync();
    //        }
    //    }

    //    async Task RunMigrationsAsync(this WebApplication app)
    //    {
    //        //await using var transaction = await dbContext.Database.BeginTransactionAsync();

    //        //await dbContext.Database.MigrateAsync();
    //        //await transaction.CommitAsync();
    //        using IServiceScope scope = app.Services.CreateScope();

    //        var dbContext = scope.ServiceProvider.GetRequiredService<Appdb>();
    //        dbContext.Database.Migrate();

    //    }
    //}
}
