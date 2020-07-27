using BLL;
using BLL.Enums;
using System;
using System.Collections.Generic;

namespace DrinkManagerConsole
{
    public static class SearchDrinkConsoleUi
    {
        public static void StartSearch(List<Drink> drinksList, SearchCriterion searchCriterion)
        {
            bool continueSearch = true;
            do
            {
                Console.Clear();
                Console.WriteLine($"\nSearch drink by {searchCriterion}\n".ToUpper());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");

                List<Drink> drinksFound = null;

                switch (searchCriterion)
                {
                    case SearchCriterion.Name:
                        Console.Write($"\nEnter a drink {searchCriterion.ToString().ToLower()} to find: ");
                        drinksFound = SearchDrink.SearchByName(Console.ReadLine(), drinksList);
                        break;
                    case SearchCriterion.Ingredients:
                        Console.WriteLine("\nInstructions: \nYou can provide ONE or MORE ingredients - separated with a space. \nYou can search drinks containing ALL or ANY of provided ingredients.");
                        Console.Write("\nWould you like to display drinks containing: \n1. ALL provided ingredients \n2. ANY of provided ingredients\n(1/2) ");
                        SearchDrinkOption searchOption;
                        switch (Console.ReadKey().KeyChar)
                        {
                            case '1':
                                searchOption = SearchDrinkOption.All;
                                break;
                            case '2':
                                searchOption = SearchDrinkOption.Any;
                                break;
                            default:
                                Console.WriteLine("\nI don't know what you mean - try again :)");
                                searchOption = SearchDrinkOption.Any; //default initialization of local variable - default choice in case user fails to choose 
                                break;
                        }
                        Console.Write($"\n\nEnter a drink {searchCriterion.ToString().ToLower()} to find: ");
                        drinksFound = SearchDrink.SearchByIngredients(new SortedSet<string>(Console.ReadLine()?.Split(' ') ?? throw new InvalidOperationException()), drinksList, searchOption);
                        break;
                }

                // invoking extracted display method
                DisplaySearchResults(drinksFound);

                Console.Write($"\nContinue search by {searchCriterion.ToString().ToLower()} (y/n)? ");
                if (Console.ReadKey().KeyChar == 'n')
                {
                    continueSearch = false;
                }
            } while (continueSearch);
        }

        public static void DisplaySearchResults(List<Drink> drinksFound)
        {
            if (drinksFound == null || drinksFound.Count == 0)
            {
                Console.WriteLine("\nNo matching drinks in our database.");
            }
            else
            {
                foreach (var drink in drinksFound)
                {
                    Console.WriteLine(
                        "\n------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Name:".PadRight(20) + drink.Name.PadRight(30) + drink.AlcoholicInfo);
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

                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}
