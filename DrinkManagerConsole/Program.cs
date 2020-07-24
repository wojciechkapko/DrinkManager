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
            Console.Write("Search by name or ingredient (n/i)? ");
            if (Console.ReadLine().ToLower().Equals("n"))
            {
                SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchDrinkConsoleUi.SearchCriterion.Name);
            }
            else
            {
                SearchDrinkConsoleUi.StartSearch(drinksListGlobal, SearchDrinkConsoleUi.SearchCriterion.Ingredient);
            }
            
            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }
    }
}