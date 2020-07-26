using System;

namespace DrinkManagerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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