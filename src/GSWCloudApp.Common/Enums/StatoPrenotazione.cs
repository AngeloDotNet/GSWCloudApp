using System.ComponentModel;

namespace GSWCloudApp.Common.Enums;

/// <summary>
/// Enum representing the different states of a reservation.
/// </summary>
public enum StatoPrenotazione
{
    /// <summary>
    /// Reservation has been inserted.
    /// </summary>
    [Description("Inserita")]
    Stato1,

    /// <summary>
    /// Reservation has been confirmed.
    /// </summary>
    [Description("Confermata")]
    Stato2,

    /// <summary>
    /// Reservation is awaiting confirmation.
    /// </summary>
    [Description("In attesa di conferma")]
    Stato3,

    /// <summary>
    /// Reservation has been canceled.
    /// </summary>
    [Description("Annullata")]
    Stato4
}
