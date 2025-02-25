using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Mediator.Interfaces.Query;
using TraduzioniSvc.BusinessLayer.Extensions;
using TraduzioniSvc.BusinessLayer.Mediator.Query;
using TraduzioniSvc.Shared;

namespace TraduzioniSvc.BusinessLayer.Mediator.Handlers.Get;

public class GetTranslationsHandler : IQueryHandler<GetTranslationsQuery, TraduzioniResult>
{
    public async Task<TraduzioniResult> Handle(GetTranslationsQuery request, CancellationToken cancellationToken)
    {
        var language = request.Language.ToLower();
        var activeLanguages = new HashSet<string> { "it" };

        if (!activeLanguages.Contains(language))
        {
            throw new LanguageNotSupportedException($"Language '{language.ToUpper()}' is not supported.");
        }

        var fileName = $"translations.{language}.json";
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Traduzioni");
        var filePath = Path.Combine(directoryPath, fileName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        ConfigurationAppExtensions.CheckAndGenerateJSON(filePath);

        var response = new TraduzioniResult()
        {
            FileName = fileName,
            FileContent = await File.ReadAllBytesAsync(filePath, cancellationToken)
        };

        return response;
    }
}