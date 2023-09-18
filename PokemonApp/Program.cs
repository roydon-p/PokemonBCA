using Microsoft.Extensions.DependencyInjection;
using PokemonMatch.BusinessLogic;

namespace YourConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Injecting the dependencies to the service
            var serviceProvider = new ServiceCollection()
                //.AddLogging(logging => logging.AddConsole())
                .AddHttpClient()
                .AddSingleton<PokemonTypeEffectiveness>()
                .BuildServiceProvider();

            var pokemonTypeEffectiveness = serviceProvider.GetService<PokemonTypeEffectiveness>();

            // Get the type effectiveness for any pokemon
            Console.Write("Enter a Pokemon name: ");
            var pokemonName = Console.ReadLine();
            var effectiveness = pokemonTypeEffectiveness.GetPokemonTypeEffectiveness(pokemonName);
            Console.WriteLine(effectiveness);
        }
    }
}
