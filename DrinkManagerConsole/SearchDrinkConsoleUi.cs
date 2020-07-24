﻿using BLL;
using System;
using System.Collections.Generic;

namespace DrinkManagerConsole
{
    public static class SearchDrinkConsoleUi
    {
        public enum SearchCriterion
        {
            Name, Ingredients
        }
        
        public static void StartSearch(List<Drink> drinksList, SearchCriterion searchCriterion)
        {
            bool continueSearch = true;
            do
            {
                Console.Clear();
                Console.WriteLine($"\nSearch drink by {searchCriterion}\n".ToUpper());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

                List<Drink> drinksFound = null;
                Console.WriteLine($"\nEnter a drink {searchCriterion.ToString().ToLower()} to find: ");

                switch (searchCriterion)
                {
                    case SearchCriterion.Name:
                        drinksFound = SearchDrink.SearchByName(Console.ReadLine(), drinksList);
                        break;
                    case SearchCriterion.Ingredients:
                        Console.WriteLine("You can provide one or more ingredients - separated with a space. \nDrinks containing all provided ingredients will be displayed.");
                        drinksFound = SearchDrink.SearchByIngredients(new List<string>(Console.ReadLine().Split(' ')), drinksList);
                        break;
                }

                if (drinksFound?.Count == 0 || drinksFound == null)
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
                
                Console.Write($"\nContinue search by {searchCriterion.ToString().ToLower()} (y/n)? ");
                if (Console.ReadLine()?.ToLower() == "n")
                {
                    continueSearch = false;
                }
            } while (continueSearch);
        }
    }
}
