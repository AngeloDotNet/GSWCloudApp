using Microsoft.AspNetCore.Http.HttpResults;

namespace TraduzioniSvc.BusinessLayer.Services;

public interface ITranslateService
{
    Task<Results<FileContentHttpResult, BadRequest<string>>> GetTranslationsAsync(string language, CancellationToken cancellationToken);
}
