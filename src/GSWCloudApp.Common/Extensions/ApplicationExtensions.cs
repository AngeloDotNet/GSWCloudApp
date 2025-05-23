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
    /// Configures the default middleware and services for the application, including exception handling,
    /// status code pages, Swagger (if enabled), forwarded headers, routing, and CORS.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    /// <param name="applicationOptions">The application options containing configuration flags.</param>
    /// <param name="policyName">The name of the CORS policy to use.</param>
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