# Pokémon Type Effectiveness App

## Overview

The Pokémon Type Effectiveness App is a console application that utilizes the [PokéAPI](https://pokeapi.co/) to determine a given Pokémon's effectiveness against other Pokémon types based on their types. It calculates and displays the strengths and weaknesses of a Pokémon when battling other types. This application is written in C# and serves as a tool for Pokémon trainers to understand their Pokémon's combat advantages and disadvantages.

## How Pokémon Types Work

- Each Pokémon has one or more types, such as ground, electric, water, etc.
- Pokémon attacks do different levels of damage to other Pokémon based on their types:
  - Normal damage amounts
  - More damage (effective)
  - Less damage (not effective)
  - No damage (no effect)
- The opposite direction also needs to be considered – Pokémon of certain types will take normal/more/less/no damage from others' attacks based on those attacks' types.

## Application Features

- Accepts a Pokémon name as user input.
- Calculates and displays the Pokémon's strengths and weaknesses against other types.
- Handles exceptions and empty responses with human-readable error messages.
- Utilizes dependency injection for flexible service configuration.
- Provides unit tests to ensure code quality and reliability.

## Sample Input and Output

**Sample Input:**
```
Enter a Pokemon name: Pikachu
```
**Sample Output:**
```
Pikachu is Strong against following pokemon types: electric, flying, steel, water
Pikachu is Weak against following pokemon types: dragon, electric, grass, ground
```

## How to Use

### Prerequisites
- .NET Core SDK (Version 6 or higher)
- Git (optional)

### Installation and Usage

1. Clone this repository to your local machine (or download the ZIP file).
   ```bash
   git clone https://github.com/roydon-p/PokemonBCA.git

2. Open a terminal or command prompt and navigate to the project directory.
   ```bash
   cd PokemonBCA

3. Build the application.
   ```bash
   dotnet build

4. Navgate to the PokemonApp directory.
   ```bash
   cd PokemonApp

5. Run the application.
   ```bash
   dotnet run

6. Follow the on-screen instructions to input a Pokemon name and view its type effectiveness.
