using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GSWCloudApp.Common.Swagger;

public class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            var openApiInfo = CreateInfoForApiVersion(description);
            options.SwaggerDoc(description.GroupName, openApiInfo);
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var text = new StringBuilder("API endpoints that enable the use of this microservice.");
        var info = new OpenApiInfo()
        {
            Title = "API endpoints",
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact()
            {
                Name = "Angelo Pirola",
                Email = "angelo@aepserver.it",
                Url = new Uri("https://angelo.aepserver.it/")
            },
            License = new OpenApiLicense()
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        if (description.IsDeprecated)
        {
            text.Append(" This API version has been deprecated.");
        }

        info.Description = text.ToString();

        return info;
    }
}