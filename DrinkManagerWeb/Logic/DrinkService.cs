using BLL;
using BLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Logic
{
    public class DrinkService : IDrinkService
    {
        public IQueryable<Drink> SearchByName(string textToSearch, IQueryable<Drink> drinksListToSearch)
        {
            return drinksListToSearch
                .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase));
        }

        public IQueryable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, IQueryable<Drink> drinksListToSearch,
            SearchDrinkOption searchOption)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Drink> SearchByAlcoholContent(string alcoholicInfo, IQueryable<Drink> drinks, IQueryable<Drink> contemporaryList)
        {
            throw new System.NotImplementedException();
        }
    }
}