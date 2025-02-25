using System.Text.Json;
using GSWCloudApp.Common.Exceptions;
using GSWCloudApp.Common.Translations;
using Helpers = GSWCloudApp.Common.Helpers.JsonHelpers;

namespace TraduzioniSvc.BusinessLayer.Extensions;

public static class ConfigurationAppExtensions
{
    public static void CheckAndGenerateJSON(string fileTranslate)
    {
        if (File.Exists(fileTranslate))
        {
            File.Delete(fileTranslate);
        }

        switch (fileTranslate)
        {
            case string path when path.Contains("translations.it.json"):
                GeneraJSONItaliano(fileTranslate);
                break;
            // Aggiungi altri case per altre lingue qui
            default:
                throw new LanguageNotSupportedException("Language not supported");
        }
    }

    public static void GeneraJSONItaliano(string filePath)
    {
        var jsonTraduzioni = new TraduzioniRoot
        {
            Fields =
            [
                new TraduzioniList
                {
                    Gruppo = "Pulsanti",
                    Traduzioni =
                    [
                        new TraduzioniItem { Oggetto = "Submit", Traduzione = "Invio", Stato = true },
                        new TraduzioniItem { Oggetto = "Cancel", Traduzione = "Annulla", Stato = true }
                    ]
                }
            ]
        };

        File.WriteAllText(filePath, JsonSerializer.Serialize(jsonTraduzioni, Helpers.JsonSerializer()));
    }
}