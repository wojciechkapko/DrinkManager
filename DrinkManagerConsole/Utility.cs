using System;
using System.Collections.Generic;
using System.Linq;
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

        public static List<Drink> MenuSearchByAlcoholContent(List<Drink> drinks)
        {
            var choice = new ConsoleKeyInfo();
            do
            {
                Console.Clear();
                Console.WriteLine("Choose one of the searching criteria: ");
                Console.WriteLine("1. Alcoholic drinks");
                Console.WriteLine("2. Non alcoholic drinks");
                Console.WriteLine("3. Optional alcohol drinks");
                Console.WriteLine("4. Alcoholic and optional alcohol drinks");
                Console.WriteLine("5. Non alcoholic and optional alcohol drinks");
                Console.WriteLine("Press any other key to go back to previous menu");

                var contemporaryList = new List<Drink>();
                choice = Console.ReadKey();
                if (choice.Key == ConsoleKey.D1 || choice.Key == ConsoleKey.D2 || choice.Key == ConsoleKey.D3 || choice.Key == ConsoleKey.D4 || choice.Key == ConsoleKey.D5)
                {
                    contemporaryList = UserChoice(choice, drinks, contemporaryList);
                    return contemporaryList;
                }
                else
                {      
                    return null;
                }
            }
            while (true);
        }
        internal static List<Drink> UserChoice(ConsoleKeyInfo key, List<Drink> drinks, List<Drink> contemporaryList)
        {
            string alcoholicInfo;
            Console.Clear();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    {
                        alcoholicInfo = "Alcoholic";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        alcoholicInfo = "Non alcoholic";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        alcoholicInfo = "Optional alcohol";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        alcoholicInfo = "Alcoholic";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        alcoholicInfo = "Optional alcohol";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        break;
                    }
                case ConsoleKey.D5:
                    {
                        alcoholicInfo = "Non alcoholic";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        alcoholicInfo = "Optional alcohol";
                        contemporaryList = SearchByAlcoholContent(alcoholicInfo, drinks, contemporaryList);
                        break;
                    }
            }
            return contemporaryList;
        }
        public static List<Drink> SearchByAlcoholContent(string alcoholicInfo, List<Drink> drinks, List<Drink> contemporaryList)
        {
            foreach (Drink drink in drinks)
            {
                if (drink.AlcoholicInfo == alcoholicInfo)
                {
                    contemporaryList.Add(drink);
                }
            }
            return contemporaryList;
        }
    }
}