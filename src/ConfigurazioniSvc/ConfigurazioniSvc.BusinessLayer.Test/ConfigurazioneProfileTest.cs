using AutoMapper;
using ConfigurazioniSvc.BusinessLayer.Mapper;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using ConfigurazioniSvc.Shared.DTO;

namespace ConfigurazioniSvc.BusinessLayer.Test;

public class ConfigurazioneProfileTest
{
    public MapperConfiguration MapperConfiguration = new(cfg
        => cfg.AddProfile<ConfigurazioneMappingProfile>());

    [Fact]
    public void ConfigurazioneToConfigurazioneDto()
    {
        var configurazione = new Configurazione
        {
            Id = Guid.NewGuid(),
            FestaId = Guid.NewGuid(),
            Chiave = "Chiave",
            Valore = "Valore",
            Tipo = "Tipo",
            Posizione = 1,
            Obbligatorio = true,
            Scope = Shared.Enums.ScopoConfigurazione.None
        };

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
        var configurazioneDto = new EditConfigurazioneDto
        {
            FestaId = Guid.NewGuid(),
            Chiave = "Chiave",
            Valore = "Valore",
            Tipo = "Tipo",
            Posizione = 1,
            Obbligatorio = true,
            Scope = Shared.Enums.ScopoConfigurazione.None
        };

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