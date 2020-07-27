using BLL;
<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
=======
using BLL.Enums;
using System;
>>>>>>> master

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
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchCriterion.Name);
                        break;

                    case MenuChoice.FindByAlcoholContent:
<<<<<<< HEAD
                        {
                            SearchDrinkConsoleUi.HandleSearchDrinksByContentInConsole(drinksListGlobal);
                            break;
                        }
=======
                        break;

>>>>>>> master
                    case MenuChoice.AddCustomDrink:
                        SearchDrinkConsoleUi.StartCustomDrinkCreation(drinksListGlobal);
                        break;

                    case MenuChoice.FindByIngredient:
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchCriterion.Ingredients);
                        break;

                    case MenuChoice.UpdateDrinksFromFile:
                        SearchDrinkConsoleUi.AddMoreDrinksFromFile(drinksListGlobal);
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