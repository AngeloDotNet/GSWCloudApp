using ConfigurazioniSvc.BusinessLayer.Extensions;
using ConfigurazioniSvc.BusinessLayer.Mediator.Query;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using Microsoft.Extensions.Configuration;
using Constants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioniSvc.BusinessLayer.Mediator.Handlers.Get;

public class GetConfigurationsHandler(IConfiguration configuration) : IQueryHandler<GetConfigurationsQuery, byte[]>
{
    public async Task<byte[]> Handle(GetConfigurationsQuery request, CancellationToken cancellationToken)
    {
        var fileName = Constants.JsonConfigurations;
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        ConfigurationAppExtensions.GenerateJSON(configuration, filePath);

        return await File.ReadAllBytesAsync(filePath, cancellationToken);
    }
}