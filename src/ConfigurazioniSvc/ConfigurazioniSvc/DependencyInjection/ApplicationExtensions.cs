using Asp.Versioning;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Vault.Options;
using GSWCloudApp.Common.Vault.Service;

namespace ConfigurazioniSvc.DependencyInjection;

public static class ApplicationExtensions
{
    public static void GenerateServiceScope(out ServiceProvider serviceMemoryScopeFactory, out ServiceProvider servicePostgresScopeFactory)
    {
        serviceMemoryScopeFactory = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        servicePostgresScopeFactory = new ServiceCollection().AddEntityFrameworkNpgsql().BuildServiceProvider();
    }

    public static async Task<string> GetSqlDatabaseConnectionAsync(WebApplicationBuilder builder)
    {
        var vaultOptions = builder.Services.ConfigureAndGet<VaultOptions>(builder.Configuration, nameof(VaultOptions))
            ?? throw new InvalidOperationException("Vault options not found in configuration.");

        return await VaultService.ReadVaultSecretAsync(vaultOptions, "postgresql", "connection");
    }

    public static async Task<string> GetRedisConnectionAsync(WebApplicationBuilder builder)
    {
        var vaultOptions = builder.Services.ConfigureAndGet<VaultOptions>(builder.Configuration, nameof(VaultOptions))
            ?? throw new InvalidOperationException("Vault options not found in configuration.");

        return await VaultService.ReadVaultSecretAsync(vaultOptions, "redis", "connection");
    }

    public static RouteGroupBuilder UseVersioningApi(WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
                    .HasApiVersion(new ApiVersion(1))
                    .Build();

        var versionedApi = app
            .MapGroup("/api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
        return versionedApi;
    }

    public static void UseDevSwagger(WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    options.SwaggerEndpoint(url, description.GroupName);
                }
            });
    }
}