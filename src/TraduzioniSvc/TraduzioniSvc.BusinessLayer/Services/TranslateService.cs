using GSWCloudApp.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using TraduzioniSvc.BusinessLayer.Mediator.Query;

namespace TraduzioniSvc.BusinessLayer.Services;

public class TranslateService(ISender sender, ILogger<TranslateService> logger) : ITranslateService
{
    public async Task<Results<FileContentHttpResult, BadRequest<string>>> GetTranslationsAsync(string language, CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(new GetTranslationsQuery(language), cancellationToken);

            return TypedResults.File(result.FileContent, "application/json", result.FileName);
        }
        catch (LanguageNotSupportedException ex)
        {
            logger.LogError(ex, ex.Message);
            return TypedResults.BadRequest(ex.Message);
        }
    }
}
