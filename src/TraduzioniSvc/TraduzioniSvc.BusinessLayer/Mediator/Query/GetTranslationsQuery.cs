using GSWCloudApp.Common.Mediator.Interfaces.Query;
using TraduzioniSvc.Shared;

namespace TraduzioniSvc.BusinessLayer.Mediator.Query;

public class GetTranslationsQuery(string language) : IQuery<TraduzioniResult>
{
    public string Language { get; set; } = language;
}
