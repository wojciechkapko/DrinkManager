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
            bool isAppRunning = true;
            do
            {
                // Menu();
                // Other

                // Reference to Drinks List passed into Search class
                var searchDrinkByName = new SearchDrinkByName(DrinkLoader.AddDrinksFromFile());
                searchDrinkByName.SearchByName();

                Console.Write("Continue (y/n)? ");
                if (Console.ReadLine().ToLower() == "n")
                {
                    isAppRunning = false;
                }
            } while (isAppRunning);

            Console.WriteLine("Press anu key.");
            Console.ReadKey();
        }
    }
}