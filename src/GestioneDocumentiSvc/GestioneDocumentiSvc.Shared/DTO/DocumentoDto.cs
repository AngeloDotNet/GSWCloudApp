namespace GestioneDocumentiSvc.Shared.DTO;

public class DocumentoDto
{
    public Guid Id { get; set; }
    public Guid FestaId { get; set; }
    public string Path { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public long Length { get; set; }
    public string NomeDocumento { get; set; } = null!;
    public string Descrizione { get; set; } = null!;
}
