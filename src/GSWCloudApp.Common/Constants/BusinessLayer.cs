namespace GSWCloudApp.Common.Constants;

/// <summary>
/// Contains constants used in the business layer of the application.
/// </summary>
public static class BusinessLayer
{
    /// <summary>
    /// The default CORS policy name.
    /// </summary>
    public const string DefaultCorsPolicyName = "AllowAll";

    /// <summary>
    /// The folder where uploads are stored.
    /// </summary>
    public const string UploadsFolder = "Uploads";

    /// <summary>
    /// The folder where document uploads are stored.
    /// </summary>
    public static readonly string DocumentUploadFolder = Path.Combine(UploadsFolder, "Documents");

    /// <summary>
    /// The folder where logo uploads are stored.
    /// </summary>
    public static readonly string LogoUploadFolder = Path.Combine(UploadsFolder, "Logos");

    /// <summary>
    /// The allowed extensions for document uploads.
    /// </summary>
    public static readonly string[] AllowedDocumentExtensions = [".pdf", ".txt"];

    /// <summary>
    /// The allowed extensions for image uploads.
    /// </summary>
    public static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".gif"];

    /// <summary>
    /// The current year in UTC.
    /// </summary>
    public static readonly string CurrentYear = DateTime.UtcNow.Year.ToString();

    /// <summary>
    /// The current month in UTC, formatted as a two-digit string.
    /// </summary>
    public static readonly string CurrentMonth = DateTime.UtcNow.Month.ToString("00");
}
