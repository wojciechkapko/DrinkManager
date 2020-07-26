using BLL;
using System;
using BLL.Enums;

namespace DrinkManagerConsole
{
    public class Program
    {
        private static void Main()
        {
            // initial loading drinks list from file
            var loader = new DrinkLoader();
            var drinksListGlobal = loader.InitializeDrinksFromFile();

            // temporary app logic - to be changed when all features are ready
            bool runApp = true;
            do
            {
                Console.Clear();
                Console.Write("Search by name or ingredient or EXIT ? (n/i/e) ");
                switch (Console.ReadKey().KeyChar)
                {
                    case 'n':
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchCriterion.Name);
                        break;
                    case 'i':
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchCriterion.Ingredients);
                        break;
                    case 'e':
                        runApp = false;
                        break;
                    default:
                        Console.WriteLine("\nI don't know what you mean - try again :)");
                        break;
                }
            } while (runApp);

            Console.WriteLine("\nPress any key to close.");
            Console.ReadKey();
        }
    }
}