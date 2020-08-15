using BLL;
using System;
using System.Collections.Generic;

namespace DrinkManagerConsole
{
    public static class EditDrinkConsoleUi
    {
        public static void StartEdition(Drink selectedDrink)
        {
            var ingredients = selectedDrink.Ingredients;
            Console.WriteLine();
            Console.WriteLine("\nWhat do you want to edit?\n");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Category");
            Console.WriteLine("3. Alcoholic Info");
            Console.WriteLine("4. Glass Type");
            Console.WriteLine("5. Instruction");
            Console.WriteLine("6. Ingredients");
            Console.WriteLine("7. Add new ingredient");
            Console.WriteLine("8. Remove ingredient");

            Console.Write("\nChoice: ");
            var choice = Console.ReadKey();
            Console.WriteLine();
            int input;

            switch (choice.Key)
            {
                case ConsoleKey.D1:
                    selectedDrink.Name = UpdateInfo("name", selectedDrink.Name);
                    break;
                case ConsoleKey.D2:
                    selectedDrink.Category = UpdateInfo("category", selectedDrink.Category);
                    break;

                case ConsoleKey.D3:
                    selectedDrink.AlcoholicInfo = Utility.GetAlcoholicInfoFromConsole();
                    break;

                case ConsoleKey.D4:
                    selectedDrink.GlassType = UpdateInfo("glass type", selectedDrink.GlassType);
                    break;

                case ConsoleKey.D5:
                    selectedDrink.Instructions = UpdateInfo("instruction", selectedDrink.Instructions);
                    break;

                case ConsoleKey.D6:
                    Console.WriteLine("\nSelect ingredient to edit:");

                    input = SelectIngredient(ingredients);

                    var newName = UpdateInfo("ingredient name", ingredients[input].Name);
                    var newAmount = UpdateInfo("ingredient amount", ingredients[input].Amount);

                    ingredients[input] = new Ingredient()
                    {
                        Name = newName,
                        Amount = newAmount
                    };
                    break;
                case ConsoleKey.D7:
                    if (ingredients.Count == 15)
                    {
                        Console.WriteLine("There are already 15 ingredients. You can't add more.");
                        Console.ReadKey();
                        break;
                    }
                    ingredients.Add(new Ingredient()
                    {
                        Name = GetNewInfo("ingredient name"),
                        Amount = GetNewInfo("amount")
                    });
                    break;
                case ConsoleKey.D8:
                    Console.WriteLine("Select ingredient to remove:");

                    input = SelectIngredient(ingredients);

                    ingredients.RemoveAt(input);
                    break;
            }

        }

        private static int SelectIngredient(List<Ingredient> ingredients)
        {
            bool inputParse;
            int input;

            for(var i=0; i<ingredients.Count; i++)
            {
                PrintIngredient(i+1, ingredients[i]);
            }

            do
            {
                Console.Write("\nChoice: ");
                var choice = Console.ReadKey();
                inputParse = int.TryParse(choice.KeyChar.ToString(), out input);
            } while (inputParse == false || input < 1 || input > ingredients.Count);

            input -= 1;
            return input;
        }

        private static void PrintIngredient(int index, Ingredient ingredient)
        {
            if(!string.IsNullOrWhiteSpace(ingredient.Name))
                Console.WriteLine(index + ". " + ingredient.Name.PadRight(20).PadRight(20) + ingredient.Amount);
        }

        private static string UpdateInfo(string fieldName, string fieldValue)
        {
            Console.WriteLine($"\nOld {fieldName}: {fieldValue}");
            
            return GetNewInfo(fieldName);
        }

        private static string GetNewInfo(string fieldName)
        {
            Console.Write($"\nNew {fieldName}: ");
            string newValue;
            do
            {
                newValue = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(newValue));

            return newValue;
        }
    }
}