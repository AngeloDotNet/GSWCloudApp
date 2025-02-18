namespace GSWCloudApp.Common.Translations;

/// <summary>
/// Represents a translation item with an object, its translation, and its status.
/// </summary>
public class TraduzioniItem
{
    /// <summary>
    /// Gets or sets the object to be translated.
    /// </summary>
    public string Oggetto { get; set; } = null!;

    /// <summary>
    /// Gets or sets the translation of the object.
    /// </summary>
    public string Traduzione { get; set; } = null!;

    /// <summary>
    /// Gets or sets the status of the translation.
    /// </summary>
    public bool Stato { get; set; }
}
