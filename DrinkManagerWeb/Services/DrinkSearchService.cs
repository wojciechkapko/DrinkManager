using BLL;
using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Services
{
    public class DrinkSearchService : IDrinkSearchService
    {
        public IEnumerable<Drink> SearchByName(string textToSearch, IEnumerable<Drink> drinksListToSearch)
        {
            return drinksListToSearch
                .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase))
                ;
        }

        public IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, IEnumerable<Drink> drinksListToSearch,
            SearchDrinkOption searchOption)
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

            return drinksFound.AsEnumerable();
        }

        public IEnumerable<Drink> SearchByAlcoholContent(string alcoholicInfo, IEnumerable<Drink> drinks, IEnumerable<Drink> contemporaryList)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Drink> SortDrinks(string sortOrder, IEnumerable<Drink> drinks)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    drinks = drinks.OrderByDescending(s => s.Name);
                    break;
                default:
                    drinks = drinks.OrderBy(s => s.Name);
                    break;
            }
            return drinks;
        }
    }
}