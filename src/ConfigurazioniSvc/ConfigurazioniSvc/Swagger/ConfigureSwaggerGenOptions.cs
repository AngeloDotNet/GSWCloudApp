using System.Text;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ConfigurazioniSvc.Swagger;

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
        var text = new StringBuilder("Microservice that allows the management of global application configurations.");
        var info = new OpenApiInfo()
        {
            Title = "Configurazioni Minimal API",
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