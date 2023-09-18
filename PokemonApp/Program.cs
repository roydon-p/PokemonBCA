using Microsoft.Extensions.DependencyInjection;
using PokemonMatch.BusinessLogic;
using Microsoft.Extensions.Logging;

namespace PokemonApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Injecting the dependencies to the service
            var serviceProvider = new ServiceCollection()
                //.AddLogging(logging => logging.AddConsole())
                .AddHttpClient()
                .AddSingleton<IPokemonTypeEffectiveness, PokemonTypeEffectiveness>()
                .BuildServiceProvider();

            var pokemonTypeEffectiveness = serviceProvider.GetService<IPokemonTypeEffectiveness>();


            // Get the type effectiveness for any pokemon
            Console.Write("Enter a Pokemon name: ");
            var pokemonName = Console.ReadLine();
            var effectiveness = await pokemonTypeEffectiveness.GetPokemonTypeEffectiveness(pokemonName);
            Console.WriteLine(effectiveness);
        }
    }
}
