using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GestioneDocumentiSvc.Shared.DTO;

public class UploadDocumentoDto
{
    [BindRequired]
    public required IFormFile Documento { get; set; }

    public Guid FestaId { get; set; }
    public string NomeDocumento { get; set; } = null!;
    public string Descrizione { get; set; } = null!;
}
