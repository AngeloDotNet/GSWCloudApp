namespace GSWCloudApp.Common.Options;

/// <summary>
/// Represents the connection strings for various SQL databases.
/// </summary>
public class ConnectionStrings
{
    /// <summary>
    /// Gets or sets the connection string for the SQL Autentica database.
    /// </summary>
    public string SqlAutentica { get; set; } = null!;

    /// <summary>
    /// Gets or sets the connection string for the SQL ConfigSmtp database.
    /// </summary>
    public string SqlConfigSmtp { get; set; } = null!;

    /// <summary>
    /// Gets or sets the connection string for the SQL GestDocumenti database.
    /// </summary>
    public string SqlGestDocumenti { get; set; } = null!;

    /// <summary>
    /// Gets or sets the connection string for the SQL GestLoghi database.
    /// </summary>
    public string SqlGestLoghi { get; set; } = null!;

    /// <summary>
    /// Gets or sets the connection string for the SQL InvioEmail database.
    /// </summary>
    public string SqlInvioEmail { get; set; } = null!;
}
