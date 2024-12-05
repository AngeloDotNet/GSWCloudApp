using ConfigurazioniSvc.Shared.Enums;

namespace ConfigurazioniSvc.Shared.DTO;

/// <summary>
/// Data Transfer Object for creating configuration settings.
/// </summary>
public class CreateConfigurazioneDto
{
    /// <summary>
    /// Gets or sets the ID of the festival.
    /// </summary>
    public Guid FestaId { get; set; }

    /// <summary>
    /// Gets or sets the key of the configuration.
    /// </summary>
    public string Chiave { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the configuration.
    /// </summary>
    public string Valore { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the configuration.
    /// </summary>
    public string Tipo { get; set; } = null!;

    /// <summary>
    /// Gets or sets the position of the configuration.
    /// </summary>
    public int Posizione { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the configuration is mandatory.
    /// </summary>
    public bool Obbligatorio { get; set; }

    /// <summary>
    /// Gets or sets the scope of the configuration.
    /// </summary>
    public ScopoConfigurazione Scope { get; set; }
}