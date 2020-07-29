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
                        var nameToSearch = GetTextToSearch(searchCriterion);
                        drinksFound = SearchDrink.SearchByName(nameToSearch, drinksList);
                        break;

                    case SearchCriterion.Ingredients:
                        Console.WriteLine("\nInstructions: \nYou can provide ONE or MORE ingredients - separated with a space. \nYou can search drinks containing ALL or ANY of provided ingredients.");
                        Console.WriteLine("\nWould you like to display drinks containing: \n1. ALL provided ingredients \n2. ANY of provided ingredients\n(1/2) ");
                        var searchOption = SearchDrinkOption.All;
                        bool incorrectInputIngredientsChoice;
                        do
                        {
                            var searchOptionChoice = Console.ReadKey();
                            switch (searchOptionChoice.KeyChar)
                            {
                                case '1':
                                    searchOption = SearchDrinkOption.All;
                                    incorrectInputIngredientsChoice = false;
                                    break;

                                case '2':
                                    searchOption = SearchDrinkOption.Any;
                                    incorrectInputIngredientsChoice = false;
                                    break;

                                default:
                                    Console.WriteLine("\nPlease enter correct answer.");
                                    incorrectInputIngredientsChoice = true;
                                    break;
                            }
                        } while (incorrectInputIngredientsChoice);
                        
                        var ingredientsToSearch = GetTextToSearch(searchCriterion);
                        drinksFound = SearchDrink.SearchByIngredients(new SortedSet<string>(ingredientsToSearch.Split(' ')), drinksList, searchOption);
                        break;
                }

                // invoking extracted display method
                DisplaySearchResults(drinksFound);

                var incorrectInputEndSearch = true;
                do
                {
                    Console.Write($"\nContinue search by {searchCriterion.ToString().ToLower()} (yes: [y/enter] / no: [n/esc])? ");
                    var continueUserInput = Console.ReadKey();
                    if (continueUserInput.KeyChar == 'y' || continueUserInput.KeyChar == 'Y' || continueUserInput.Key == ConsoleKey.Enter)
                    {
                        incorrectInputEndSearch = false;
                    }
                    else if (continueUserInput.KeyChar == 'n' || continueUserInput.KeyChar == 'N' || continueUserInput.Key == ConsoleKey.Escape)
                    {
                        incorrectInputEndSearch = false;
                        continueSearch = false;
                    }
                } while (incorrectInputEndSearch);
                
            } while (continueSearch);
        }

        private static string GetTextToSearch(SearchCriterion searchCriterion)
        {
            string textToSearch;
            var incorrectInput = true;
            do
            {
                Console.Write($"\nEnter a drink {searchCriterion.ToString().ToLower()} to find: ");
                textToSearch = Console.ReadLine();
                if (textToSearch != "")
                {
                    incorrectInput = false;
                }
            } while (incorrectInput);
            
            return textToSearch;
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

        public static void StartCustomDrinkCreation(List<Drink> drinksList)
        {
            var creator = new DrinkCreator();
            creator.AddNewDrink(drinksList);

            Console.WriteLine("\nDrink added.");
            // This should be replaced with a method maybe like "WaitForAnyKey(string message)"
            Console.WriteLine("\nPress any key to go back to the main menu.");
            Console.ReadKey();
        }

        public static void AddMoreDrinksFromFile(List<Drink> drinksList)
        {
            Console.WriteLine("Please provide full path to the source file");
            var path = Console.ReadLine();
            var loader = new DrinkLoader();
            try
            {
                loader.AddDrinksFromFile(drinksList, path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}");
                // This should be replaced with a method maybe like "WaitForAnyKey(string message)"
                Console.WriteLine("\nPress any key to go back to the main menu.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("New drinks added!");
            // This should be replaced with a method maybe like "WaitForAnyKey(string message)"
            Console.WriteLine("\nPress any key to go back to the main menu.");
            Console.ReadKey();
        }
    }
}