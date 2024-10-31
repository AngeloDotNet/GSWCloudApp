using ConfigurazioniSvc.Shared.Enums;

namespace ConfigurazioniSvc.Shared.DTO;

public class ConfigurazioneDto
{
    public Guid Id { get; set; }
    public Guid FestaId { get; set; }
    public string? Valore { get; set; }
    public string? Tipo { get; set; }
    public int Posizione { get; set; }
    public bool Obbligatorio { get; set; }
    public ScopoConfigurazione Scope { get; set; }
}
