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
    }
}