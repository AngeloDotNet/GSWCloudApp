using System.ComponentModel;

namespace GSWCloudApp.Common.Enums;

public enum StatoPrenotazione
{
    [Description("Inserita")]
    Stato1,

    [Description("Confermata")]
    Stato2,

    [Description("In attesa di conferma")]
    Stato3,

    [Description("Annullata")]
    Stato4
}
