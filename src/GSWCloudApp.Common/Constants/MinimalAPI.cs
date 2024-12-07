namespace GSWCloudApp.Common.Constants;

/// <summary>
/// Contains route patterns for the Minimal API.
/// </summary>
public class MinimalAPI
{
    /// <summary>
    /// The route pattern for identifying an entity by its GUID.
    /// </summary>
    public const string PatternById = "{id:guid}";

    /// <summary>
    /// The route pattern for filtering entities by a specific GUID.
    /// </summary>
    public const string PatternFilterById = "/filter/{festaid:guid}";
}
