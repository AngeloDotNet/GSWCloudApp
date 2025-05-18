using Asp.Versioning;
using GSWCloudApp.Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;

namespace GSWCloudApp.Common.Extensions;

/// <summary>
/// Provides extension methods for configuring the application.
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    /// Configures API versioning for the application.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>A <see cref="RouteGroupBuilder"/> for further configuration.</returns>
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

    /// <summary>
    /// Configures Swagger for the application in development environment or if enabled in options.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <param name="options">The application options.</param>
    [Obsolete("UseDevSwagger is deprecated, please use UseDefaultServices instead.")]
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

    /// <summary>
    /// Configures the application to use forwarded headers.
    /// </summary>
    /// <param name="app">The web application.</param>
    [Obsolete("UseForwardNetworking is deprecated, please use UseDefaultServices instead.")]
    public static void UseForwardNetworking(this WebApplication app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        // Non necessario se viene usato NGINX come proxy
        // app.UseHttpsRedirection();
    }

    public static void UseDefaultServices(this WebApplication app, ApplicationOptions applicationOptions, string policyName)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();

        if (app.Environment.IsDevelopment() || applicationOptions.SwaggerEnable)
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

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        // Non necessario se viene usato NGINX come proxy
        // app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors(policyName);
    }
}
