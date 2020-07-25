using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class SearchDrink
    {
        public static List<Drink> SearchByName(string textToSearch, List<Drink> drinksListToSearch)
        {
            return drinksListToSearch
                .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        public static List<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, List<Drink> drinksListToSearch, SearchEnums.SearchDrinkOption searchOption)
        {
            var drinksFound = new List<Drink>();
            var ingredientsFound = new SortedSet<string>();

            foreach (var drink in drinksListToSearch)
            {
                foreach (var drinkIngredient in drink.Ingredients)
                {
                    if (drinkIngredient.Name == null)
                    {
                        continue;
                    }

                    foreach (var ingredient in ingredientsToSearch)
                    {
                        if (drinkIngredient.Name.Contains(ingredient, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ingredientsFound.Add(ingredient);
                        }
                    }
                }

                if (ingredientsFound.SetEquals(ingredientsToSearch))
                {
                    drinksFound.Add(drink);
                }

                ingredientsFound.Clear();
            }
            return drinksFound;
        }
    }
}
