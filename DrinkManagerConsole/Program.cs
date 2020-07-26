using BLL;
﻿using System;
using System.Collections.Generic;

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
                        {
                            Utility.MenuSearchByAlcoholContent();
                            var searchChoice = Console.ReadKey();
                            if(searchChoice.Key == ConsoleKey.D1 || searchChoice.Key == ConsoleKey.D2 || searchChoice.Key == ConsoleKey.D3 || searchChoice.Key == ConsoleKey.D4 || searchChoice.Key == ConsoleKey.D5)
                            {
                               var contemporaryList = Utility.GetDrinksByAlcoholContent(searchChoice, drinksListGlobal);
                            }
                            break; 
                        }
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