using GSWCloudApp.Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;

namespace GSWCloudApp.Common.Extensions;

public static class ApplicationExtensions
{
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