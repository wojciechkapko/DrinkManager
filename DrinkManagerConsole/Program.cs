using System;

namespace DrinkManagerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var drinkManagerApplication = new DrinkManagerApplication(); // parameter-less constructor loads drinks data from file to internal variable: drinksListGlobal
            drinkManagerApplication.Start();  // start of application
           
            Console.WriteLine("Press anu key.");
            Console.ReadKey();
        }
    }
}