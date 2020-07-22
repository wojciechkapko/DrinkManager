using BLL;
using System;
using System.Collections.Generic;

namespace DrinkManagerConsole
{
    public static class SearchDrinkByNameConsole
    {
        public static void StartSearch(List<Drink> drinksList)
        {
            bool continueSearch = true;
            do
            {
                Console.Clear();
                Console.WriteLine("\nSearch drink by name");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

                // Text to search and reference to Drinks List passed into Search method
                var drinksFound = SearchDrinkByName.SearchByName(Utility.GetGenericData("Enter a drink name to find: "), drinksList);

                if (drinksFound.Count == 0)
                {
                    Console.WriteLine("\nNo matching drinks in our database.");
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
