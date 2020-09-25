using BLL;
using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Services
{
    public class DrinkSearchService : IDrinkSearchService
    {
        public IQueryable<Drink> SearchByName(string textToSearch, IQueryable<Drink> drinksListToSearch)
        {
            return drinksListToSearch
                .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase));
        }

        public IQueryable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, IQueryable<Drink> drinksListToSearch,
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

            return drinksFound.AsQueryable();
        }

        public IQueryable<Drink> SearchByAlcoholContent(string alcoholicInfo, IQueryable<Drink> drinks, IQueryable<Drink> contemporaryList)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Drink> SortDrinks(string sortOrder, IQueryable<Drink> drinks)
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