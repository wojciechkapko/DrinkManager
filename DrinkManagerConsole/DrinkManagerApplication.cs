using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DrinkManagerConsole
{
    public class DrinkManagerApplication
    {
        public readonly List<Drink> DrinksListGlobal;

        public DrinkManagerApplication()
        {
            DrinksListGlobal = DrinkLoader.AddDrinksFromFile();
        }

        public void Start()
        {
        }
    }
}
