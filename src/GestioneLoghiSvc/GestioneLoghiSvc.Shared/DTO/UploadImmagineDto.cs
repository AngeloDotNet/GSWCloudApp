using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GestioneLoghiSvc.Shared.DTO;

public class UploadImmagineDto
{
    [BindRequired]
    public IFormFile Immagine { get; set; }

    public Guid FestaId { get; set; }
    public string NomeDocumento { get; set; } = null!;
    public string Descrizione { get; set; } = null!;
}
