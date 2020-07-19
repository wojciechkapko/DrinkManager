using System;
using BLL;

namespace DrinkManagerConsole
{
    internal class SearchDrink
    {
        internal static void SearchDrinkByName()
        {
            Console.WriteLine("\nSearch drink by name\n");
            var textToSearch = Utility.GetGenericData("Enter a drink name to find: ");

        }
    }
}
