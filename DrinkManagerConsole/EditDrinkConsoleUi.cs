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
            Console.WriteLine("What do you want to edit?");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Category");
            Console.WriteLine("3. Alcoholic Info");
            Console.WriteLine("4. Glass Type");
            Console.WriteLine("5. Instruction");
            Console.WriteLine("6. Ingredients");
            Console.WriteLine("7. Add new ingredient");
            Console.WriteLine("8. Remove ingredient");
            Console.WriteLine();

            var input = 0;
            var inputParse = false;
            do
            {
                Console.WriteLine("Choice: ");
                inputParse = int.TryParse(Console.ReadLine(), out input);
            } while (inputParse == false || input < 1 || input > 8);

            switch (input)
            {
                case 1:
                    selectedDrink.Name = UpdateInfo("name", selectedDrink.Name);
                    break;
                case 2:
                    selectedDrink.Category = UpdateInfo("category", selectedDrink.Category);
                    break;

                case 3:
                    selectedDrink.AlcoholicInfo = Utility.GetAlcoholicInfoFromConsole();
                    break;

                case 4:
                    selectedDrink.GlassType = UpdateInfo("glass type", selectedDrink.GlassType);
                    break;

                case 5:
                    selectedDrink.Instructions = UpdateInfo("instruction", selectedDrink.Instructions);
                    break;

                case 6:
                    Console.WriteLine("Select ingredient to edit:");

                    input = SelectIngredient(ingredients);

                    var newName = UpdateInfo("ingredient name", ingredients[input].Name);
                    var newAmount = UpdateInfo("ingredient amount", ingredients[input].Amount);

                    ingredients[input] = new Ingredient()
                    {
                        Name = newName,
                        Amount = newAmount
                    };
                    selectedDrink.Ingredients = ingredients;
                    break;
                case 7:
                    var index = -1;
                    for (var i = 0; i < ingredients.Count; i++)
                    {
                        if (string.IsNullOrWhiteSpace(ingredients[i].Name))
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        Console.WriteLine("There are already 15 ingredients. You can't add more.");
                        break;
                    }
                    newName = GetNewInfo("ingredient name");
                    newAmount = GetNewInfo("amount");

                    ingredients[index] = new Ingredient()
                    {
                        Name = newName,
                        Amount = newAmount
                    };
                    selectedDrink.Ingredients = ingredients;
                    break;
                case 8:
                    Console.WriteLine("Select ingredient to remove:");

                    input = SelectIngredient(ingredients);

                    ingredients.RemoveAt(input);
                    ingredients.Add(new Ingredient());
                    selectedDrink.Ingredients = ingredients;
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

            Console.WriteLine();
            do
            {
                Console.WriteLine("Choice: ");
                inputParse = int.TryParse(Console.ReadLine(), out input);
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
            Console.WriteLine($"Old {fieldName}: {fieldValue}.");
            
            return GetNewInfo(fieldName);
        }

        private static string GetNewInfo(string fieldName)
        {
            Console.Write($"New {fieldName}: ");
            string newValue;
            do
            {
                newValue = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(newValue));

            return newValue;
        }
    }
}