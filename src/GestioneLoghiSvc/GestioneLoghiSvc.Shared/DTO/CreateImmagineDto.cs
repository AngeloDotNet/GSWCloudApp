namespace GestioneLoghiSvc.Shared.DTO;

public class CreateImmagineDto
{
    public Guid FestaId { get; set; }
    public string Path { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public long Length { get; set; }
    public string NomeImmagine { get; set; } = null!;
    public string Descrizione { get; set; } = null!;
}