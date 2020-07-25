using BLL;
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

            // temporary app logic - to be changed when all features are ready
            bool runApp = true;
            do
            {
                Console.Write("Search by name or ingredient or EXIT ? (n/i/e) ");
                switch (Console.ReadLine())
                {
                    case "n":
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchDrinkConsoleUi.SearchCriterion.Name);
                        break;
                    case "i":
                        SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchDrinkConsoleUi.SearchCriterion.Ingredients);
                        break;
                    case "e":
                        runApp = false;
                        break;
                    default:
                        Console.WriteLine("I don't know what you mean - try again :)");
                        break;
                }
            } while (runApp);

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }
    }
}