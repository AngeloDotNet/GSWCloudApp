namespace GSWCloudApp.Common.Translations;

/// <summary>
/// Represents a list of translation items grouped by a specific category.
/// </summary>
public class TraduzioniList
{
    /// <summary>
    /// Gets or sets the group name for the translation items.
    /// </summary>
    public string Gruppo { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of translation items.
    /// </summary>
    public List<TraduzioniItem> Traduzioni { get; set; } = [];
}
