using System.ComponentModel;

namespace GSWCloudApp.Common.Enums;

/// <summary>
/// Represents the status of a receipt.
/// </summary>
public enum StatoScontrino
{
    /// <summary>
    /// The receipt is open.
    /// </summary>
    [Description("Aperto")]
    Stato1,

    /// <summary>
    /// The receipt is being processed.
    /// </summary>
    [Description("In elaborazione")]
    Stato2,

    /// <summary>
    /// The receipt is ready to be cashed.
    /// </summary>
    [Description("Da incassare")]
    Stato3,

    /// <summary>
    /// The receipt has been paid.
    /// </summary>
    [Description("Pagato")]
    Stato4,

    /// <summary>
    /// The receipt is closed.
    /// </summary>
    [Description("Chiuso")]
    Stato5,

    /// <summary>
    /// The receipt has been canceled.
    /// </summary>
    [Description("Annullato")]
    Stato6
}