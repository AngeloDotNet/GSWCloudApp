using AutoMapper;
using Bogus;
using ConfigurazioniSvc.BusinessLayer.Mapper;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using ConfigurazioniSvc.Shared.DTO;
using ConfigurazioniSvc.Shared.Enums;

namespace ConfigurazioniSvc.BusinessLayer.Test;

public class ConfigurazioneProfileTest
{
    public MapperConfiguration MapperConfiguration = new(cfg
        => cfg.AddProfile<MappingProfile>());

    private static Faker<Configurazione> GetConfigurazioneFaker()
    {
        return new Faker<Configurazione>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.FestaId, f => f.Random.Guid())
            .RuleFor(c => c.Chiave, f => f.Lorem.Word())
            .RuleFor(c => c.Valore, f => f.Lorem.Word())
            .RuleFor(c => c.Tipo, f => f.Lorem.Word())
            .RuleFor(c => c.Posizione, f => f.Random.Int(1, 100))
            .RuleFor(c => c.Obbligatorio, f => f.Random.Bool())
            .RuleFor(c => c.Scope, f => f.PickRandom<ScopoConfigurazione>());
    }

    private static Faker<CreateConfigurazioneDto> GetCreateConfigurazioneDtoFaker()
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

    private static Faker<EditConfigurazioneDto> GetEditConfigurazioneDtoFaker()
    {
        return new Faker<EditConfigurazioneDto>()
            .RuleFor(c => c.FestaId, f => f.Random.Guid())
            .RuleFor(c => c.Chiave, f => f.Lorem.Word())
            .RuleFor(c => c.Valore, f => f.Lorem.Word())
            .RuleFor(c => c.Tipo, f => f.Lorem.Word())
            .RuleFor(c => c.Posizione, f => f.Random.Int(1, 100))
            .RuleFor(c => c.Obbligatorio, f => f.Random.Bool())
            .RuleFor(c => c.Scope, f => f.PickRandom<ScopoConfigurazione>());
    }

    [Fact]
    public void ConfigurazioneToConfigurazioneDto()
    {
        var configurazione = GetConfigurazioneFaker().Generate();

        var mapper = MapperConfiguration.CreateMapper();
        var configurazioneDto = mapper.Map<ConfigurazioneDto>(configurazione);

        Assert.Equal(configurazione.Id, configurazioneDto.Id);
        Assert.Equal(configurazione.FestaId, configurazioneDto.FestaId);
        Assert.Equal(configurazione.Valore, configurazioneDto.Valore);
        Assert.Equal(configurazione.Tipo, configurazioneDto.Tipo);
        Assert.Equal(configurazione.Posizione, configurazioneDto.Posizione);
        Assert.Equal(configurazione.Obbligatorio, configurazioneDto.Obbligatorio);
        Assert.Equal(configurazione.Scope, configurazioneDto.Scope);
    }

    [Fact]
    public void CreateConfigurazioneDtoToConfigurazione()
    {
        var configurazioneDto = GetCreateConfigurazioneDtoFaker().Generate();

        var mapper = MapperConfiguration.CreateMapper();
        var configurazione = mapper.Map<Configurazione>(configurazioneDto);

        Assert.Equal(configurazioneDto.FestaId, configurazione.FestaId);
        Assert.Equal(configurazioneDto.Valore, configurazione.Valore);
        Assert.Equal(configurazioneDto.Tipo, configurazione.Tipo);
        Assert.Equal(configurazioneDto.Posizione, configurazione.Posizione);
        Assert.Equal(configurazioneDto.Obbligatorio, configurazione.Obbligatorio);
        Assert.Equal(configurazioneDto.Scope, configurazione.Scope);
    }

    [Fact]
    public void EditConfigurazioneDtoToConfigurazione()
    {
        var configurazioneDto = GetEditConfigurazioneDtoFaker().Generate();

        var mapper = MapperConfiguration.CreateMapper();
        var configurazione = mapper.Map<Configurazione>(configurazioneDto);

        Assert.Equal(configurazioneDto.FestaId, configurazione.FestaId);
        Assert.Equal(configurazioneDto.Valore, configurazione.Valore);
        Assert.Equal(configurazioneDto.Tipo, configurazione.Tipo);
        Assert.Equal(configurazioneDto.Posizione, configurazione.Posizione);
        Assert.Equal(configurazioneDto.Obbligatorio, configurazione.Obbligatorio);
        Assert.Equal(configurazioneDto.Scope, configurazione.Scope);
    }
}
