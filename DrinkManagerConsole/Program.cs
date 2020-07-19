using System;

namespace DrinkManagerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //test code block - to be deleted
            var search = new SearchDrinkByName();
            search.SearchByName();
            Console.WriteLine("Press anu key.");
            Console.ReadKey();
            //end of test code block
        }
    }
}