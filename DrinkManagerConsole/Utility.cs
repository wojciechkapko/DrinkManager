using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;

namespace DrinkManagerConsole
{
    internal static class Utility
    {
        public static string GetAlcoholicInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Is this an Alcoholic drink?: ");
            Console.WriteLine();
            Console.WriteLine("1. Alcoholic");
            Console.WriteLine("2. Non Alcoholic");
            Console.WriteLine("3. Optional Alcoholic");

            return GetAlcoholicInfoChoice();
        }

        private static string GetAlcoholicInfoChoice()
        {
            var choice = new ConsoleKeyInfo();

            do
            {
                choice = Console.ReadKey(true);
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        return "Alcoholic";

                    case ConsoleKey.D2:
                        return "Non alcoholic";

                    case ConsoleKey.D3:
                        return "Optional alcohol";

                    default:
                        Console.WriteLine("\nUnsupported input, try again...\n");
                        continue;
                }
            } while (choice.Key != ConsoleKey.D1 || choice.Key != ConsoleKey.D2);

            return "";
        }

        public static string GetGenericData(string message = null)
        {
            if (message != null)
            {
                Console.WriteLine();
                Console.Write(message);
            }

            return Console.ReadLine();
        }

        public static List<Ingredient> GetIngredients()
        {
            var ingredients = new List<Ingredient>();

            Console.WriteLine();
            Console.Write("Number of ingredients: ");
            var numberOfIngredients = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numberOfIngredients; i++)
            {
                Console.WriteLine();
                Console.Write("Name: ");
                var name = Console.ReadLine();
                Console.Write("Amount: ");
                var amount = Console.ReadLine();

                ingredients.Add(new Ingredient()
                {
                    Name = name,
                    Amount = amount
                });
            }

            return ingredients;
        }

        public static void SearchByAlcoholContent(List<Drink> drinks)
        {
            var emptyDrinks = new List<Drink>();
            var choice = new ConsoleKeyInfo();
            if(drinks == emptyDrinks)
            {
                Console.WriteLine("The drink list is currently empty - Try to update your data by loading drinks from a file!");
            }
            do
            {
                Console.WriteLine("Choose one of the searching criteria: ");
                Console.WriteLine("1. Alcoholic drinks");
                Console.WriteLine("2. Non alcoholic drinks");
                Console.WriteLine("3. Optional alcohol drinks");
                Console.WriteLine("4. Alcoholic and optional alcohol drinks");
                Console.WriteLine("5. Non alcoholic and optional alcohol drinks");
                Console.WriteLine("Press any other key to go back to previous menu");

                var contemporaryDrinkList = new List<Drink>();
                choice = Console.ReadKey(true);
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        SearchAlcoholic(drinks, contemporaryDrinkList);
                        break;

                    case ConsoleKey.D2:
                        SearchNonAlcoholic(drinks, contemporaryDrinkList);
                        break;

                    case ConsoleKey.D3:
                        SearchOptionalAlcohol(drinks, contemporaryDrinkList);
                        break;

                    case ConsoleKey.D4:
                        SearchAlcoholicAndOptionalAlcohol(drinks, contemporaryDrinkList);
                        break;

                    case ConsoleKey.D5:
                        SearchNonAndOptionalAlcohol(drinks, contemporaryDrinkList);
                        break;

                    default:
                        return;
                }
            } while (choice.Key != ConsoleKey.D1 || choice.Key != ConsoleKey.D2);
        }
        public static void SearchAlcoholic(List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == "Alcoholic")
                {
                    contemporaryList.Add(drink);
                }
            }
            CheckDrink(contemporaryList);
        }
        public static void SearchNonAlcoholic(List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == "Non alcoholic")
                {
                    contemporaryList.Add(drink);
                }
            }
            CheckDrink(contemporaryList);
        }
        public static void SearchOptionalAlcohol(List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == "Optional alcohol")
                {
                    contemporaryList.Add(drink);
                }
            }
            CheckDrink(contemporaryList);
        }
        public static void SearchNonAndOptionalAlcohol(List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == "Non alcoholic")
                {
                    contemporaryList.Add(drink);
                }
            }
            SearchOptionalAlcohol(drinks, contemporaryList);
        }
        public static void SearchAlcoholicAndOptionalAlcohol(List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == "Non alcoholic")
                {
                    contemporaryList.Add(drink);
                }
            }
            SearchOptionalAlcohol(drinks, contemporaryList);
        }
        public static void CheckDrink(List<Drink> contemporaryList)
        {
            var choice = new ConsoleKeyInfo();
            int page = 0;
            int index = 0;
            int counter = 0;

            while (index < contemporaryList.Count)
            {
                if (counter % 10 == 0 && counter != 0)
                {
                    page++;
                    counter = 0;
                    do
                    {
                        Console.WriteLine("\nTo check any drink info press its corresponding number. \nTo go to next page press N to go back to previous page press P.\nTo go back to previous menu press any other key");
                        choice = Console.ReadKey();
                        switch (choice.Key)
                        {
                            case ConsoleKey.D1:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D2:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 1)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D3:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 2)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D4:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 3)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D5:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 4)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D6:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 5)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D7:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 6)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D8:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 7)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D9:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 8)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.N:
                                break;
                            case ConsoleKey.P:
                                if (page == 0)
                                    break;
                                else
                                {
                                    page--;
                                    break;
                                }
                            default:
                                return;
                        }
                    }
                    while (choice.Key != ConsoleKey.N || (choice.Key != ConsoleKey.P && page != 0));
                }
                else
                {
                    Console.WriteLine($"{counter}.".PadRight(6 ,' ') + contemporaryList.ElementAt<Drink>(index).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(index).AlcoholicInfo.PadRight(12, ' '));
                }
                counter++;
                index = counter + page * 10;
                if(index == contemporaryList.Count)
                {
                    int number = 0;
                    do
                    {
                        Console.WriteLine("\nTo check any drink info press its corresponding number. \nTo go back to previous page press P.\nTo go back to previous menu press any other key");
                        choice = Console.ReadKey();    
                        int.TryParse(choice.KeyChar.ToString(), out number);
                        if (number > index - page * 10)
                        {
                            return;
                        } 
                        switch (choice.Key)
                        {
                            case ConsoleKey.D1:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D2:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 1)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D3:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 2)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D4:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 3)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D5:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 4)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D6:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 5)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D7:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 6)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D8:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 7)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D9:
                                Console.WriteLine($"{contemporaryList.ElementAt<Drink>(page * 9 + 8)}");
                                Console.WriteLine("To go back to previous menu, press any key");
                                Console.ReadKey();
                                break;
                            case ConsoleKey.P:
                                if (page == 0)
                                    break;
                                else
                                {
                                    page--;
                                    index = index - (counter + 10);
                                    counter = 0;
                                    break;
                                }
                            default:
                                return;
                        }
                    }
                    while (choice.Key != ConsoleKey.P);
                }
            }

        }
    }
}