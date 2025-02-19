namespace GSWCloudApp.Common.Constants;

/// <summary>
/// Contains route patterns for the Minimal API.
/// </summary>
public static class MinimalApi
{
    /// <summary>
    /// The route pattern for an empty path.
    /// </summary>
    public const string PatternEmpty = "";

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

    /// <summary>
    /// The route pattern for specifying a language.
    /// </summary>
    public const string PatternLanguage = "/{language}";

    /// <summary>
    /// The route pattern for user login.
    /// </summary>
    public const string PatternLogin = "/login";

    /// <summary>
    /// The route pattern for refreshing a token.
    /// </summary>
    public const string PatternRefreshToken = "/refresh-token";

    /// <summary>
    /// The route pattern for user registration.
    /// </summary>
    public const string PatternRegister = "/register";
}
