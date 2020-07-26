using BLL;
using BLL.Enums;
using System;

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
                        break;

                    case MenuChoice.AddCustomDrink:
                        SearchDrinkConsoleUi.StartCustomDrinkCreation(drinksListGlobal);
                        break;

                    case MenuChoice.FindByIngredient:
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchCriterion.Ingredients);
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