using System;
namespace PokemonMatch.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<PokemonType>? Types { get; set; }
    }
}

