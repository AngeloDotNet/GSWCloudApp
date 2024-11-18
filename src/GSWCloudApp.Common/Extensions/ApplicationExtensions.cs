using Asp.Versioning;
using GSWCloudApp.Common.Options;
using GSWCloudApp.Common.Vault.Options;
using GSWCloudApp.Common.Vault.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace GSWCloudApp.Common.Extensions;

public static class ApplicationExtensions
{
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

    public static void UseDevSwagger(this WebApplication app, ApplicationOptions options)
    {
        if (app.Environment.IsDevelopment() || options.SwaggerEnable)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
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