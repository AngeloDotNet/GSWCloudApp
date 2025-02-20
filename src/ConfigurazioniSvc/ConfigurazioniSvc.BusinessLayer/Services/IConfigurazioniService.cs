using Microsoft.AspNetCore.Http.HttpResults;

namespace ConfigurazioniSvc.BusinessLayer.Services;

public interface IConfigurazioniService
{
    Task<Results<FileContentHttpResult, BadRequest<string>>> GetConfigurationsAsync(CancellationToken cancellationToken);
}