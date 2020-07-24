using System;
using System.Collections.Generic;

namespace BLL
{
    public static class SearchDrink
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

        public static List<Drink> SearchByIngredient(string ingredientToSearch, List<Drink> drinksListToSearch)
        {
            var drinksFound = new List<Drink>();

            foreach (var drink in drinksListToSearch)
            {
                foreach (var ingredient in drink.Ingredients)
                {
                    if (ingredient.Name == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (ingredient.Name.Contains(ingredientToSearch, StringComparison.InvariantCultureIgnoreCase))
                        {
                            drinksFound.Add(drink);
                        }
                    }
                }
            }

            return drinksFound;
        }
    }
}
