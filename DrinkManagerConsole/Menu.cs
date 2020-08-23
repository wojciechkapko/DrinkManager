using System;
using System.Collections.Generic;

namespace DrinkManagerConsole
{
    internal enum MenuChoice
    {
        FindByName = 1,
        FindByAlcoholContent = 2,
        FindByIngredient = 3,
        AddCustomDrink = 4,
        UpdateDrinksFromFile = 5,
        DisplayFavourites = 6,
        ShowReviewedDrinksList = 7,
        Exit = 8
    }

    internal class Menu
    {
        private readonly string[] _lines = {
            @"  ____                    _          ,__ __                                      ",
            @" (|   \       o          | |        /|  |  |                                     ",
            @"  |    | ,_       _  _   | |   ,     |  |  |   __,   _  _    __,   __,  _   ,_   ",
            @" _|    |/  |  |  / |/ |  |/_) / \_   |  |  |  /  |  / |/ |  /  |  /  | |/  /  |  ",
            @"(/\___/    |_/|_/  |  |_/| \_/ \/    |  |  |_/\_/|_/  |  |_/\_/|_/\_/|/|__/   |_/",
            @"                                                                    /|           ",
            @"                                                                    \|           ",
        };

        private readonly Dictionary<MenuChoice, string> _options = new Dictionary<MenuChoice, string>
        {
            {MenuChoice.FindByName, "Find drink by its name."},
            {MenuChoice.FindByAlcoholContent, "Find drink by alcohol content."},
            {MenuChoice.FindByIngredient, "Find drink by its ingredient."},
            {MenuChoice.AddCustomDrink, "Add your own drink."},
            {MenuChoice.UpdateDrinksFromFile, "Update your drinks data."},
            {MenuChoice.DisplayFavourites, "Display your favourite drinks."},
	        {MenuChoice.ShowReviewedDrinksList, "Show your reviewed drinks."},
            {MenuChoice.Exit, "Exit."}
        };

        public void Show()
        {
            Console.Clear();

            foreach (var line in _lines)
            {
                Console.WriteLine(line);
            }

            foreach (var option in _options)
            {
                Console.WriteLine($"{(int)option.Key}. {option.Value}");
            }

            Console.WriteLine();
        }

        public MenuChoice GetUserChoice()
        {
            Console.Write("Choose option: ");

            while (true)
            {
                var userInput = Console.ReadKey();
                Console.WriteLine();
                if (char.IsDigit(userInput.KeyChar))
                {
                    var choice = int.Parse(userInput.KeyChar.ToString());
                    if (choice >= 1 && choice <= _options.Count)
                    {
                        return (MenuChoice)choice;
                    }
                }
                Console.Write("Please choose one of possible options: ");
            }
        }
    }
}
