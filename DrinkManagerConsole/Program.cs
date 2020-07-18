using System;
using System.Collections.Generic;
using BLL;

namespace DrinkManagerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var drinkManager = new DrinkManager();
            drinkManager.Run();
        }
    }
}