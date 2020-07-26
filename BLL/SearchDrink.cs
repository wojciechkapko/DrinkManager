using BLL.Enums;
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

        public static List<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, List<Drink> drinksListToSearch, SearchDrinkOption searchOption)
        {
            var drinksFound = new List<Drink>();
            var ingredientsFound = new SortedSet<string>();

            if (searchOption == SearchDrinkOption.All)
            {
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
            }
            else if (searchOption == SearchDrinkOption.Any)
            {
                foreach (var drink in drinksListToSearch)
                {
                    var nextDrink = false;

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
                                drinksFound.Add(drink);
                                nextDrink = true;
                                break;
                            }
                        }

                        if (nextDrink)
                        {
                            break;
                        }
                    }
                }
            }

            return drinksFound;
        }
    }
}
