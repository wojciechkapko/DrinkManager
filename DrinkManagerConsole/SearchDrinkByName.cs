using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DrinkManagerConsole
{
    internal class SearchDrinkByName
    {
        //private readonly List<Drink> drinksList;

        //public SearchDrinkByName()
        //{
        //    drinksList = LoadFromFile();
        //}

        internal void SearchByName()
        {
            Console.Clear();
            Console.WriteLine("\nSearch drink by name");
            Console.WriteLine("-------------------------------");
            Console.Write("Enter a drink name to find: ");

            int numberOfDrinksFound = 0;
            string textToSearch;
            do
            {
                textToSearch = Console.ReadLine();
            } while (textToSearch == null);

            foreach (var drink in drinksList)
            {
                if (drink.Name.ToLower().Contains(textToSearch.ToLower()))
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------------");
                    Console.WriteLine($"Name: {drink.Name}");
                    Console.WriteLine($"Is alcoholic: {drink.AlcoholicInfo}");
                    Console.WriteLine($"Category: {drink.Category}");
                    Console.WriteLine($"Glass type: {drink.GlassType}");
                    Console.WriteLine($"Ingredients: ");
                    foreach (var ingredient in drink.Ingredients)
                    {
                        if (ingredient.Name == null) continue;
                        Console.WriteLine($"{ingredient.Name} : {ingredient.Amount}");
                    }
                    Console.WriteLine($"Instructions: {drink.Instructions}");
                    numberOfDrinksFound++;
                }
            }

            if (numberOfDrinksFound == 0)
            {
                Console.WriteLine("No matching drinks in our database.");
            }
        }

        
    }
}
