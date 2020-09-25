using BLL;
using BLL.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Services
{
    public interface IDrinkSearchService
    {
        IQueryable<Drink> SearchByName(string textToSearch, IQueryable<Drink> drinksListToSearch);

        IQueryable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch,
            IQueryable<Drink> drinksListToSearch, SearchDrinkOption searchOption);

        IQueryable<Drink> SearchByAlcoholContent(string alcoholicInfo, IQueryable<Drink> drinks,
            IQueryable<Drink> contemporaryList);

        IQueryable<Drink> SortDrinks(string sortOrder, IQueryable<Drink> drinks);
    }
}
