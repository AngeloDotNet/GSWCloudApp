using ConfigurazioniSvc.BusinessLayer.Validation;
using ConfigurazioniSvc.Shared.DTO;

namespace ConfigurazioniSvc.BusinessLayer.Test;

public class ConfigurazioneValidationTest
{
    [Fact]
    public void CreateConfigurazioneDtoValidationIsValid()
    {
        var configurazioneDto = new CreateConfigurazioneDto
        {
            FestaId = Guid.NewGuid(),
            Chiave = "Chiave",
            Valore = "Valore",
            Tipo = "Tipo",
            Posizione = 1,
            Obbligatorio = true,
            Scope = Shared.Enums.ScopoConfigurazione.None
        };

        var validator = new CreateConfigurazioneValidator();
        var result = validator.Validate(configurazioneDto);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateConfigurazioneDtoValidationFailed()
    {
        var configurazioneDto = new CreateConfigurazioneDto
        {
            FestaId = Guid.Empty,
            Chiave = "",
            Valore = "",
            Tipo = "",
            Posizione = 0,
            Obbligatorio = false,
            Scope = Shared.Enums.ScopoConfigurazione.None
        };

        var validator = new CreateConfigurazioneValidator();
        var result = validator.Validate(configurazioneDto);

        Assert.False(result.IsValid);
        Assert.Equal(4, result.Errors.Count);
        Assert.Equal("'Festa Id' must not be empty.", result.Errors.ToList()[0].ErrorMessage);
    }
}