using BLL;
using System.Collections.Generic;

namespace DrinkManagerWeb.Data
{
    public class DrinkAppContext
    {
        public DrinkAppContext(DrinkLoader loader)
        {
            Drinks = loader.InitializeDrinksFromFile();
        }
        
        public List<Drink> Drinks { get; private set; }

        public List<int> FavouriteDrinksIds { get; set; } = new List<int>();

    }
}