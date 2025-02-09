namespace GSWCloudApp.Common.Constants;

/// <summary>
/// Contains route patterns for the Minimal API.
/// </summary>
public static class MinimalApi
{
    /// <summary>
    /// The route pattern for uploading files.
    /// </summary>
    public const string PatternUpload = "/upload";

    /// <summary>
    /// The route pattern for downloading files.
    /// </summary>
    public const string PatternDownload = "/download/{fileName}";

    /// <summary>
    /// The route pattern for deleting a document by its file name.
    /// </summary>
    public const string PatternDeleteDocument = "/{fileName}";

    /// <summary>
    /// The route pattern for deleting an image by its file name.
    /// </summary>
    public const string PatternDeleteImage = "/{fileName}";
}