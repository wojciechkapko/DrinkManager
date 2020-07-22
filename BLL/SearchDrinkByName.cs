using System;
using System.Collections.Generic;

namespace BLL
{
    public static class SearchDrinkByName
    {
        public static List<Drink> SearchByName(string textToSearch, List<Drink> drinksListToSearch)
        {
            var drinksFound = new List<Drink>();

            foreach (var drink in drinksListToSearch)
            {
                if (drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase))
                {
                    drinksFound.Add(drink);
                }
            }

            return drinksFound;
        }
    }
}
