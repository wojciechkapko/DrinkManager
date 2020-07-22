using BLL;
using System;

namespace DrinkManagerConsole
{
    public class Program
    {
        private static void Main()
        {
            // initial loading drinks list from file into static variable in Drink Loader class
            DrinkLoader.AddDrinksFromFile();

            // temporary app logic - to be changed when all features are ready 
            SearchDrinkByNameConsole.StartSearch();

            Console.WriteLine("Press anu key.");
            Console.ReadKey();
        }
    }
}