using System;
using System.Collections.Generic;
using System.Text;
using BLL;

namespace DrinkManagerConsole
{
    internal class DrinkManager
    {
        private readonly List<Drink> _drinks = new List<Drink>();
        private readonly DrinkLoader _drinkLoader;

        public DrinkManager()
        {
            _drinkLoader = new DrinkLoader(_drinks);
        }

        public void Run()
        {
            //Menu
        }
    }
}