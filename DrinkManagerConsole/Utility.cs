using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace DrinkManagerConsole
{
    internal static class Utility
    {
        public static string GetAlcoholicInfoFromConsole()
        {
            Console.WriteLine("\nIs this an Alcoholic drink?: ");
            Console.WriteLine("\n1. Alcoholic");
            Console.WriteLine("2. Non Alcoholic");
            Console.WriteLine("3. Optional Alcoholic");

            ConsoleKeyInfo choice;

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

            return "Something went wrong";
        }

        public static string GetGenericData(string message = null)
        {
            if (message != null)
            {
                Console.Write($"\n{message}");
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