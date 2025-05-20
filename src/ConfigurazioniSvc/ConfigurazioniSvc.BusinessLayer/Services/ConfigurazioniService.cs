using ConfigurazioniSvc.BusinessLayer.Mediator.Query;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Constants = GSWCloudApp.Common.Constants.BusinessLayer;

namespace ConfigurazioniSvc.BusinessLayer.Services;

public class ConfigurazioniService(ILogger<ConfigurazioniService> logger, IQueryHandler<GetConfigurationsQuery, byte[]> handler) : IConfigurazioniService
{
    public async Task<Results<FileContentHttpResult, BadRequest<string>>> GetConfigurationsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var fileName = Constants.JsonConfigurations;
            var result = await handler.Handle(new GetConfigurationsQuery(), cancellationToken);

            return TypedResults.File(result, "application/json", fileName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return TypedResults.BadRequest(ex.Message);
        }
    }
}
