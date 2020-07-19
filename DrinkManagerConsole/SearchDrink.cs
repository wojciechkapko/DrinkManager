using System;
using BLL;

namespace DrinkManagerConsole
{
    internal class SearchDrink
    {
        internal static void SearchDrinkByName()
        {
            Console.Clear();
            Console.WriteLine("\nSearch drink by name\n");
            Console.Write("Enter a drink name to find: ");
            var textToSearch = Console.ReadLine();
            // load drinks list from file
            //search for text in names
            //return result to the console
        }
    }
}
