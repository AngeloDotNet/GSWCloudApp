using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace GSWCloudApp.Common.Helpers;

/// <summary>
/// Provides helper methods for application configuration.
/// </summary>
public static class ApplicationHelpers
{
    /// <summary>
    /// Applies pending migrations for the specified DbContext.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="app">The web application.</param>
    public static async Task ApplyMigrationsAsync<TDbContext>(this WebApplication app) where TDbContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        if (!await dbCreator.ExistsAsync())
        {
            await dbCreator.CreateAsync();
        }

        await dbContext.Database.MigrateAsync();
    }
}
