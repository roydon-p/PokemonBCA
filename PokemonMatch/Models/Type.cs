using System;
using Newtonsoft.Json;

namespace PokemonMatch.Models
{
    public class Type
    {
        public int Id { get; set; }

        [JsonProperty("damage_relations")]
        public TypeRelations? DamageRelations { get; set; }
    }
}

