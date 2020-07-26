using BLL;
﻿using System;

namespace DrinkManagerConsole
{
    public class Program
    {
        private static void Main()
        {
            // initial loading drinks list from file
            var loader = new DrinkLoader();
            var drinksListGlobal = loader.InitializeDrinksFromFile();

            var exitProgram = false;
            do
            {
                var menu = new Menu();
                menu.Show();
                var choice = menu.GetUserChoice();

                switch (choice)
                {
                    case MenuChoice.FindByName:
                        break;
                    case MenuChoice.FindByAlcoholContent:
                        break;
                    case MenuChoice.AddCustomDrink:
                        break;
                    case MenuChoice.FindByIngredient:
                        break;
                    case MenuChoice.UpdateDrinksFromFile:
                        break;
                    case MenuChoice.Exit:
                        exitProgram = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            } while (exitProgram == false);
        }
    }
}