using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PokemonMatch.Models;
using Type = PokemonMatch.Models.Type;

namespace PokemonMatch.BusinessLogic
{
    public class PokemonTypeEffectiveness : IPokemonTypeEffectiveness
    {
        private readonly ILogger<PokemonTypeEffectiveness> _log;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string BaseUrl = "https://pokeapi.co/api/v2/";

        // Constructor used for Dependency Injection of HTTP Client and Logger
        public PokemonTypeEffectiveness(ILogger<PokemonTypeEffectiveness> log, IHttpClientFactory httpClientFactory)
        {
            _log = log;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Used by other Projects to get the Type Effectiveness of a Pokemon.
        /// </summary>
        /// <param name="pokemonName">The pokemon name input.</param>
        /// <returns>The string containing the pokemon's strengths and weaknesses.</returns>
        public string GetPokemonTypeEffectiveness(string pokemonName)
        {
            try
            {
                Pokemon pokemon = GetPokemonDetails(pokemonName);
                if (pokemon == null)
                {
                    return "Could not find information for " + pokemonName;
                }
                List<TypeRelations> allDamageRelations = GetAllDamageRelations(pokemon.Types);
                string pokemonEffectiveness = BuildEffectivenessString(pokemonName, allDamageRelations);
                return pokemonEffectiveness;
            }
            catch (Exception ex)
            {
                _log.LogError("An error occurred: " + ex.Message);
                return "An error occurred: " + ex.Message;
            }
        }

        /// <summary>
        /// Gets the Pokemon details by using the pokemon name.
        /// </summary>
        /// <param name="pokemonName">The pokemon name input.</param>
        /// <returns>The Pokemon object deserialized from the json returned by the API.</returns>
        private Pokemon GetPokemonDetails(string pokemonName)
        {
            try
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    HttpResponseMessage response = httpClient.GetAsync($"{BaseUrl}pokemon/{pokemonName}/").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<Pokemon>(jsonResponse);
                }
            }
            catch (HttpRequestException ex)
            {
                _log.LogError("HTTP request error occurred: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets all the damage relations for the input pokemons types.
        /// </summary>
        /// <param name="pokemonTypes">The List of types for current pokemon.</param>
        /// <returns>The List of Damage relations for current pokemons types.</returns>
        private List<TypeRelations> GetAllDamageRelations(List<PokemonType> pokemonTypes)
        {
            List<TypeRelations> allDamageRelations = new List<TypeRelations>();

            foreach (var pokeType in pokemonTypes)
            {
                TypeRelations damageRelations = GetPokemonTypeRelationsForType(pokeType.Type.Name);
                if (damageRelations != null)
                {
                    allDamageRelations.Add(damageRelations);
                }
            }
            return allDamageRelations;
        }

        /// <summary>
        /// Gets the damage relations for the input pokemons given type.
        /// </summary>
        /// <param name="pokemonType">The type for current pokemon.</param>
        /// <returns>The Damage relations for current pokemons type.</returns>
        private TypeRelations GetPokemonTypeRelationsForType(string pokemonType)
        {
            try
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    HttpResponseMessage response = httpClient.GetAsync($"{BaseUrl}type/{pokemonType}/").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;

                    Type typeDetails = JsonConvert.DeserializeObject<Type>(jsonResponse);

                    return typeDetails.DamageRelations;
                }
            }
            catch (HttpRequestException ex)
            {
                _log.LogError("HTTP request error occurred: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Builds the string of strengths and weaknesses of current pokemon.
        /// </summary>
        /// <param name="pokemonName">The name of current input pokemon.</param>
        /// <param name="allDamageRelations">The list of all damage relations for current input pokemon.</param>
        /// <returns>string containing the effectiveness of the pokemon</returns>
        private string BuildEffectivenessString(string pokemonName, List<TypeRelations> allDamageRelations)
        {
            List<string> strength = new List<string>();
            List<string> weakness = new List<string>();

            foreach (var damageRelation in allDamageRelations)
            {
                strength.AddRange(damageRelation.DoubleDamageTo.ConvertAll(x => x.Name));
                strength.AddRange(damageRelation.NoDamageFrom.ConvertAll(x => x.Name));
                strength.AddRange(damageRelation.HalfDamageFrom.ConvertAll(x => x.Name));

                weakness.AddRange(damageRelation.DoubleDamageFrom.ConvertAll(x => x.Name));
                weakness.AddRange(damageRelation.NoDamageTo.ConvertAll(x => x.Name));
                weakness.AddRange(damageRelation.HalfDamageTo.ConvertAll(x => x.Name));
            }

            strength.Sort();
            weakness.Sort();

            string effectiveness = $"{pokemonName} is Strong against following pokemon types: {string.Join(", ", strength.Distinct())}\n";
            effectiveness += $"{pokemonName} is Weak against following pokemon types: {string.Join(", ", weakness.Distinct())}\n";

            return effectiveness;
        }
    }
}

