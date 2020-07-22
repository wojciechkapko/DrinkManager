using BLL;
using System;

namespace DrinkManagerConsole
{
    public static class SearchDrinkByNameConsole
    {
        public static void StartSearch()
        {
            bool continueSearch = true;
            do
            {
                Console.Clear();
                Console.WriteLine("\nSearch drink by name");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                // Reference to Drinks List passed into Search class using constructor with 1 parameter
                var searchDrinkByName = new SearchDrinkByName(DrinkLoader.AddDrinksFromFile());
                var drinksFound = searchDrinkByName.SearchByName(Utility.GetGenericData("Enter a drink name to find: "));

                if (drinksFound.Count == 0)
                {
                    Console.WriteLine("No matching drinks in our database.");
                }
                else
                {
                    foreach (var drink in drinksFound)
                    {
                        Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"Name: {drink.Name}");
                        Console.WriteLine($"Is alcoholic: {drink.AlcoholicInfo}");
                        Console.WriteLine($"Category: {drink.Category}");
                        Console.WriteLine($"Glass type: {drink.GlassType}");
                        Console.WriteLine("Ingredients: ");
                        foreach (var ingredient in drink.Ingredients)
                        {
                            if (ingredient.Name == null) continue;
                            Console.WriteLine($"{ingredient.Name} : {ingredient.Amount}");
                        }
                        Console.WriteLine($"Instructions: {drink.Instructions}");
                    }
                }
                
                Console.Write("\nContinue search (y/n)? ");
                if (Console.ReadLine()?.ToLower() == "n")
                {
                    continueSearch = false;
                }
            } while (continueSearch);
        }
    }
}
