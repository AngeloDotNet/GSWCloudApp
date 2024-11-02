using ConfigurazioniSvc.Shared.Enums;

namespace ConfigurazioniSvc.Shared.DTO;

public class CreateConfigurazioneDto
{
    public Guid FestaId { get; set; }
    public string Chiave { get; set; } = null!;
    public string Valore { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public int Posizione { get; set; }
    public bool Obbligatorio { get; set; }
    public ScopoConfigurazione Scope { get; set; }
}