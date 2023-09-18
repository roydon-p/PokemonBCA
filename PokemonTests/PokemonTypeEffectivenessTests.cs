using Microsoft.Extensions.Logging;
using PokemonMatch.BusinessLogic;
using Moq;
using PokemonMatch.Models;
using System.Net.Http;

namespace PokemonTests;

public class PokemonTypeEffectivenessTests
{
    [Fact]
    public async Task GetPokemonTypeEffectiveness_ValidPokemon_SingleType()
    {
        var logger = new Mock<ILogger<PokemonTypeEffectiveness>>();
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var httpClient = new Mock<HttpClient>();

        httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient.Object);

        var pokemonTypeEffectiveness = new PokemonTypeEffectiveness(logger.Object, httpClientFactory.Object);

        string result = await pokemonTypeEffectiveness.GetPokemonTypeEffectiveness("ditto");
        Console.WriteLine(result);
        string expected = "ditto is Strong against following pokemon types: ghost\nditto is Weak against following pokemon types: fighting, ghost, rock, steel\n";
        string actual = result;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetPokemonTypeEffectiveness_ValidPokemon_DualType()
    {
        var logger = new Mock<ILogger<PokemonTypeEffectiveness>>();
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var httpClient = new Mock<HttpClient>();

        httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient.Object);

        var pokemonTypeEffectiveness = new PokemonTypeEffectiveness(logger.Object, httpClientFactory.Object);

        string result = await pokemonTypeEffectiveness.GetPokemonTypeEffectiveness("Bulbasaur");
        Console.WriteLine(result);
        string expected = "Bulbasaur is Strong against following pokemon types: bug, electric, fairy, fighting, grass, ground, poison, rock, water\nBulbasaur is Weak against following pokemon types: bug, dragon, fire, flying, ghost, grass, ground, ice, poison, psychic, rock, steel\n";
        string actual = result;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetPokemonTypeEffectiveness_InvalidPokemon()
    {
        var logger = new Mock<ILogger<PokemonTypeEffectiveness>>();
        var httpClientFactory = new Mock<IHttpClientFactory>();
        var httpClient = new Mock<HttpClient>();

        httpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient.Object);

        var pokemonTypeEffectiveness = new PokemonTypeEffectiveness(logger.Object, httpClientFactory.Object);

        string result = await pokemonTypeEffectiveness.GetPokemonTypeEffectiveness("Pikachuu");
        Console.WriteLine(result);
        string expected = "Could not find information for Pikachuu";
        string actual = result;
        Assert.Equal(expected, actual);
    }
}
