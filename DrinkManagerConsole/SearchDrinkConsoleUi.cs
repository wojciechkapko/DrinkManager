using BLL;
using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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

                if (drinksFound != null && drinksFound.Count == 0)
                {
                    Console.WriteLine("\nNo drinks found in the database matching your criteria.");
                }
                else
                {
                    PagingHandler.DivideDrinkListIntoPages(drinksFound);
                }

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
            do
            {
                Console.Write($"\nEnter a drink {searchCriterion.ToString().ToLower()} to find: ");
                textToSearch = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(textToSearch));

            return textToSearch;
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
            Console.WriteLine("Please provide full path to the source file.");
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

        /// <summary>
        /// Shows search criteria menu, gets user input and cause GetDrinksByAlcoholContent to run
        /// </summary>
        /// <param name="drinks"></param>
        public static void HandleSearchDrinksByContentInConsole(List<Drink> drinks)
        {
            Console.Clear();
            Console.WriteLine("Choose one of the searching criteria: ");
            Console.WriteLine("\n1. Alcoholic drinks");
            Console.WriteLine("2. Non alcoholic drinks");
            Console.WriteLine("3. Optional alcohol drinks");
            Console.WriteLine("4. Alcoholic and optional alcohol drinks");
            Console.WriteLine("5. Non alcoholic and optional alcohol drinks");
            Console.WriteLine("\nPress any other key to go back to previous menu");

            var searchChoice = Console.ReadKey();
            if (searchChoice.Key == ConsoleKey.D1 || searchChoice.Key == ConsoleKey.D2 || searchChoice.Key == ConsoleKey.D3 || searchChoice.Key == ConsoleKey.D4 || searchChoice.Key == ConsoleKey.D5)
            {
                PagingHandler.DivideDrinkListIntoPages(GetDrinksByAlcoholContent(searchChoice, drinks));
                Console.Clear();
            }
        }

        /// <summary>
        /// Depending on user input from HandleSearchDrinksByContentinConsole, causes SearchByAlcoholContent to run with specified search criteria
        /// </summary>
        /// <param name="key"></param>
        /// <param name="drinks"></param>
        /// <returns></returns>
        public static List<Drink> GetDrinksByAlcoholContent(ConsoleKeyInfo key, List<Drink> drinks)
        {
            var contemporaryList = new List<Drink>();
            Console.Clear();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    {
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Alcoholic", drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Non alcoholic", drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Optional alcohol", drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Alcoholic", drinks, contemporaryList);
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Optional alcohol", drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D5:
                    {
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Non alcoholic", drinks, contemporaryList);
                        contemporaryList = SearchDrink.SearchByAlcoholContent("Optional alcohol", drinks, contemporaryList);
                        break;
                    }
            }
            return contemporaryList;
        }

        public static void PrintDrinksOnPage(List<Drink> contemporaryList, int counter, int index)
        {
            Console.WriteLine($"{counter}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(index).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(index).AlcoholicInfo.PadRight(12, ' '));
        }

        public static void PrintInstructionWhileOnPagedList(List<Drink> contemporaryList, int page)
        {
            if (page * 9 + 9 > contemporaryList.Count)
            {
                if (page == 0)
                {
                    Console.WriteLine("\nPress corresponding number to select drink." +
                                      "\nPress ESC to go back to main menu.");
                }
                else
                {
                    Console.WriteLine("\nPress corresponding number to select drink." +
                                      "\nPress P to go to previous page." +
                                      "\nPress ESC to go back to main menu.");
                }
            }
            else if (page == 0)
            {
                Console.WriteLine("\nPress corresponding number to select drink." +
                                  "\nPress N to go to next page." +
                                  "\nPress ESC to go back to main menu.");
            }
            else
            {
                Console.WriteLine("\nPress corresponding number to select drink." +
                                  "\nPress N to go to next page or press P to go to previous page." +
                                  "\nPress ESC to go back to main menu.");
            }
        }

        public static void TellUserThatHeCanNotGoToNextPage(int page)
        {
            if (page == 0)
            {
                Console.WriteLine("\nThis is the only page, there is no more pages.");
            }
            else
            {
                Console.WriteLine("\nThis is the last page, there is no more.");
            }
        }

        public static void TellUserThatHeCanNotGoBack(List<Drink> contemporaryList, int page)
        {
            if (page * 9 + 9 > contemporaryList.Count)
            {
                Console.WriteLine("\nThis is the one and only page.");
            }
            else
            {
                Console.WriteLine("\nThis is the first page.");
            }
        }

        public static void WriteExceptionCaughtInfo()
        {
            Console.WriteLine("Exception caught: ArgumentOutOfRangeException. Next time choose the number that is on the list!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void ReWriteDrinkListOnConsole(List<Drink> contemporaryList, int page, ConsoleKeyInfo choice)
        {
            Console.Clear();
            //Print next page of drinks if user pressed key for Next Page
            if (choice.Key == ConsoleKey.N)
            {
                for (int i = page * 9; i < contemporaryList.Count; i++)
                {
                    Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ')
                                      + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ')
                                      + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                }
            }
            //Print previous page of drinks if user pressed key for Previous Page
            else if (choice.Key == ConsoleKey.P)
            {
                for (int i = page * 9; i < Math.Min(9, contemporaryList.Count); i++)
                {
                    Console.WriteLine($"{i + 1}.".PadRight(6, ' ')
                                      + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ')
                                      + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                }
            }
            //Reprints list after drink check or pressing unsupported input
            else
            {
                for (int i = page * 9; i < Math.Min(page * 9 + 9, contemporaryList.Count); i++)
                {
                    Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ')
                                      + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ')
                                      + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                }
            }
        }

        public static void WriteDrinkInfo(Drink drink)
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
            Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------");
            if (drink.isReviewed)
            {
                Console.WriteLine($"Score: {drink.DrinkReview.ReviewScore}");
                Console.WriteLine(drink.DrinkReview.ReviewText);
                Console.WriteLine($"Date: {drink.DrinkReview.ReviewDate}");
                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------");
            }
        }

        public static void DisplayProductDetailsOptions(Drink drink)
        {
            while (true)
            {
                Console.WriteLine();
                if (!drink.isReviewed)
                {
                    Console.WriteLine("Press R to leave a review for this drink.");
                }
                else if (ValidateReviewAge(drink.DrinkReview.ReviewDate))
                {
                    Console.WriteLine("Press R to edit review for this drink.");
                }
                Console.WriteLine("Press F to add to favorites list.");
                Console.WriteLine("Press E to edit drink details.");
                Console.WriteLine("\nPress ESC to go back.");

                var userChoice = Console.ReadKey(true);

                switch (userChoice.Key)
                {
                    case ConsoleKey.R:
                        if (!drink.isReviewed || (drink.isReviewed && ValidateReviewAge(drink.DrinkReview.ReviewDate)))
                        {
                            ReviewUi.GetDataFromUserForNewReview(drink);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nUnsupported input, try again...\n");
                            continue;
                        }

                    case ConsoleKey.F:
                        // Add to favorites method call
                        FavouritesService.AddDrink(drink);
                        break;

                    case ConsoleKey.E:
                        EditDrinkConsoleUi.StartEdition(drink);
                        break;

                    case ConsoleKey.Escape:
                        return;

                    default:
                        Console.WriteLine("\nUnsupported input, try again...\n");
                        continue;
                }
            }
        }
        public static void ShowReviewedDrinksHandler(List<Drink> drinks)
        {
            var reviewService = new ReviewService();
            Console.Clear();
            PagingHandler.DivideDrinkListIntoPages(reviewService.ShowReviewed(drinks));
        }

        private static bool ValidateReviewAge(DateTime reviewDate) => !(DateTime.Now.Subtract(reviewDate).TotalSeconds > 60);
    }
}