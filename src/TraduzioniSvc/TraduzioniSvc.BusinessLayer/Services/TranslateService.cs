using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using TraduzioniSvc.BusinessLayer.Mediator.Query;
using TraduzioniSvc.Shared;

namespace TraduzioniSvc.BusinessLayer.Services;

public class TranslateService(ILogger<TranslateService> logger, IQueryHandler<GetTranslationsQuery, TraduzioniResult> handler) : ITranslateService
{
    public async Task<Results<FileContentHttpResult, BadRequest<string>>> GetTranslationsAsync(string language, CancellationToken cancellationToken)
    {
        try
        {
            var result = await handler.Handle(new GetTranslationsQuery(language), cancellationToken);

            return TypedResults.File(result.FileContent, "application/json", result.FileName);
        }
        catch (LanguageNotSupportedException ex)
        {
            logger.LogError(ex, ex.Message);
            return TypedResults.BadRequest(ex.Message);
        }
    }
}
