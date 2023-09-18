namespace PokemonMatch.BusinessLogic
{
    public interface IPokemonTypeEffectiveness
    {
        Task<string> GetPokemonTypeEffectiveness(string pokemonName);
    }
}