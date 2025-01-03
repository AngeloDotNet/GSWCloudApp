using Bogus;
using ConfigurazioniSvc.BusinessLayer.Validation;
using ConfigurazioniSvc.Shared.DTO;
using ConfigurazioniSvc.Shared.Enums;

namespace ConfigurazioniSvc.BusinessLayer.Test;

public class ConfigurazioneValidationTest
{
    private static Faker<CreateConfigurazioneDto> CreateConfigurazioneDtoFaker()
    {
        return new Faker<CreateConfigurazioneDto>()
            .RuleFor(c => c.FestaId, f => f.Random.Guid())
            .RuleFor(c => c.Chiave, f => f.Lorem.Word())
            .RuleFor(c => c.Valore, f => f.Lorem.Word())
            .RuleFor(c => c.Tipo, f => f.Lorem.Word())
            .RuleFor(c => c.Posizione, f => f.Random.Int(1, 100))
            .RuleFor(c => c.Obbligatorio, f => f.Random.Bool())
            .RuleFor(c => c.Scope, f => f.PickRandom<ScopoConfigurazione>());
    }

    private static Faker<CreateConfigurazioneDto> CreateConfigurazioneDtoInvalidFaker()
    {
        return new Faker<CreateConfigurazioneDto>()
            .RuleFor(c => c.FestaId, f => Guid.Empty)
            .RuleFor(c => c.Chiave, f => string.Empty)
            .RuleFor(c => c.Valore, f => string.Empty)
            .RuleFor(c => c.Tipo, f => string.Empty)
            .RuleFor(c => c.Posizione, f => 0)
            .RuleFor(c => c.Obbligatorio, f => true)
            .RuleFor(c => c.Scope, f => ScopoConfigurazione.None);
    }

    [Fact]
    public void CreateConfigurazioneDtoValidationIsValid()
    {
        var configurazioneDto = CreateConfigurazioneDtoFaker();

        var validator = new CreateConfigurazioneValidator();
        var result = validator.Validate(configurazioneDto);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateConfigurazioneDtoValidationFailed()
    {
        var configurazioneDto = CreateConfigurazioneDtoInvalidFaker();

        var validator = new CreateConfigurazioneValidator();
        var result = validator.Validate(configurazioneDto);

        Assert.False(result.IsValid);
        Assert.Equal(4, result.Errors.Count);
        Assert.Equal("'Festa Id' must not be empty.", result.Errors.ToList()[0].ErrorMessage);
    }
}