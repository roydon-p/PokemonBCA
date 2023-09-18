using System;
using Newtonsoft.Json;

namespace PokemonMatch.Models
{
    public class TypeRelations
    {
        [JsonProperty("no_damage_to")]
        public List<NamedAPIResource>? NoDamageTo { get; set; }

        [JsonProperty("no_damage_from")]
        public List<NamedAPIResource>? NoDamageFrom { get; set; }

        [JsonProperty("half_damage_to")]
        public List<NamedAPIResource>? HalfDamageTo { get; set; }

        [JsonProperty("half_damage_from")]
        public List<NamedAPIResource>? HalfDamageFrom { get; set; }

        [JsonProperty("double_damage_to")]
        public List<NamedAPIResource>? DoubleDamageTo { get; set; }

        [JsonProperty("double_damage_from")]
        public List<NamedAPIResource>? DoubleDamageFrom { get; set; }
    }
}

