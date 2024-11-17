using Asp.Versioning;
using GSWCloudApp.Common.Extensions;
using GSWCloudApp.Common.Vault.Options;
using GSWCloudApp.Common.Vault.Service;
using Microsoft.AspNetCore.HttpOverrides;

namespace ConfigurazioniSvc.DependencyInjection;

public static class ApplicationExtensions
{
    //public static void GenerateServiceScope(out ServiceProvider serviceMemoryScopeFactory, out ServiceProvider servicePostgresScopeFactory)
    //{
    //    serviceMemoryScopeFactory = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
    //    servicePostgresScopeFactory = new ServiceCollection().AddEntityFrameworkNpgsql().BuildServiceProvider();
    //}

    public static async Task<string> GetVaultStringConnectionAsync(WebApplicationBuilder builder, string vaultPath, string vaultKey)
    {
        var vaultOptions = builder.Services.ConfigureAndGet<VaultOptions>(builder.Configuration, nameof(VaultOptions))
            ?? throw new InvalidOperationException("Vault options not found.");

        return await VaultService.ReadVaultSecretAsync(vaultOptions, vaultPath, vaultKey);
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

    public static void UseDevSwagger(this WebApplication app)
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

    public static void UseForwardNetworking(this WebApplication app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        // Non necessario se viene usato NGINX come proxy
        // app.UseHttpsRedirection();
    }

    public static void UseAuthentication(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}