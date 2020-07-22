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
                Console.WriteLine("\nSearch drink by name\n".ToUpper());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

                // Text to search and reference to Drinks List passed into Search method - returns list
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
                        Console.WriteLine("Name:".PadRight(20) + drink.Name.PadRight(20) + drink.AlcoholicInfo);
                        Console.WriteLine("Category:".PadRight(20) + drink.Category);
                        Console.WriteLine("Glass type:".PadRight(20) + drink.GlassType);
                        Console.WriteLine("\nIngredients: ");
                        foreach (var ingredient in drink.Ingredients)
                        {
                            if (ingredient.Name == null)
                            {
                                continue;
                            }
                            Console.WriteLine(ingredient.Name.PadRight(20) + ingredient.Amount);
                        }
                        Console.WriteLine($"\nInstructions:\n{drink.Instructions}");
                    }
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
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
