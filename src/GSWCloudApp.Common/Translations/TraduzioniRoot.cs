namespace GSWCloudApp.Common.Translations;

/// <summary>
/// Represents the root object containing a list of translation lists.
/// </summary>
public class TraduzioniRoot
{
    /// <summary>
    /// Gets or sets the list of translation lists.
    /// </summary>
    public List<TraduzioniList> Fields { get; set; } = [];
}
