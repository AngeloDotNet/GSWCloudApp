using System.Net;
using System.Net.Http.Json;
using ConfigurazioniSvc.DataAccessLayer;
using ConfigurazioniSvc.DataAccessLayer.Entities;
using ConfigurazioniSvc.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurazioniSvc.IntegrationTest;

public class IntegrationTest
{
    [Fact]
    public async Task Get_Configurazioni_Should_Response_With_Ok_Status_CodeAsync()
    {
        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var itemConfigurazione = GenerateFakeData();

                    context.Configurazioni.AddRange(itemConfigurazione);
                    context.SaveChanges();
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.GetAsync("/api/v1/configurazioni?cacheData=false");
        var items = await response.Content.ReadFromJsonAsync<List<ConfigurazioneDto>>();

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.All(items!, i => context.Configurazioni.Select(t => t.Id).Contains(i.Id));
    }

    [Fact]
    public async Task Post_Configurazioni_Should_Response_With_Created_Status_Code_And_Should_Return_The_Created_ItemAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var model = GenerateFakeSingleData();
        var response = await httpClient.PostAsJsonAsync("/api/v1/configurazioni", model);
        var responseContent = await response.Content.ReadFromJsonAsync<ConfigurazioneDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(model.Chiave, responseContent!.Chiave);
        Assert.NotEqual(Guid.Empty, responseContent!.Id);
    }

    [Fact]
    public async Task Post_Configurazioni_Should_Response_With_Unprocessable_Entity_Status_Code_And_Should_Return_The_Validation_ErrorsAsync()
    {
        using var app = new ApiWebApplicationFactory();

        var httpClient = app.CreateClient();
        var model = GenerateFakeInvalidSingleData();
        var response = await httpClient.PostAsJsonAsync("/api/v1/configurazioni", model);
        var responseContent = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        Assert.Contains(nameof(model.FestaId), responseContent!.Errors.Keys);
        Assert.Equal("'Festa Id' must not be empty.", responseContent.Errors[nameof(model.FestaId)].First());
    }

    [Fact]
    public async Task Delete_Configurazione_Should_Response_With_Ok_Status_Code_And_Remove_Configurazione_Item_CorrectlyAsync()
    {
        var itemId = Guid.Empty;

        using var app = new ApiWebApplicationFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    using var scope = services.BuildServiceProvider().CreateScope();
                    using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var itemConfigurazione = GenerateFakeSingleData();

                    context.Configurazioni.Add(itemConfigurazione);
                    context.SaveChanges();

                    itemId = itemConfigurazione.Id;
                });
            });

        var httpClient = app.CreateClient();
        var response = await httpClient.DeleteAsync($"/api/v1/configurazioni/{itemId}");

        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.DoesNotContain(context.Configurazioni, t => t.Id == itemId);
    }

    #region "Generazione dati fake per i test"

    private static List<Configurazione> GenerateFakeData()
    {
        var listConfigurazione = new List<Configurazione>();

        for (var i = 0; i < 10; i++)
        {
            var itemConfigurazione = new Configurazione
            {
                FestaId = Guid.NewGuid(),
                Chiave = $"Chiave {i}",
                Valore = $"Valore {i}",
                Tipo = $"Tipo {i}",
                Posizione = i,
                Obbligatorio = true,
                Scope = Shared.Enums.ScopoConfigurazione.None
            };

            listConfigurazione.Add(itemConfigurazione);
        }

        return listConfigurazione;
    }

    private static Configurazione GenerateFakeSingleData()
    {
        var random = new Random();
        var value = random.Next(11, 30);

        return new Configurazione
        {
            FestaId = Guid.NewGuid(),
            Chiave = $"Chiave {value}",
            Valore = $"Valore {value}",
            Tipo = $"Tipo {value}",
            Posizione = value,
            Obbligatorio = true,
            Scope = Shared.Enums.ScopoConfigurazione.None
        };
    }

    private static Configurazione GenerateFakeInvalidSingleData()
    {
        return new Configurazione
        {
            FestaId = Guid.Empty,
            Chiave = "",
            Valore = "",
            Tipo = "",
            Posizione = 0,
            Obbligatorio = true,
            Scope = Shared.Enums.ScopoConfigurazione.None
        };
    }

    #endregion
}