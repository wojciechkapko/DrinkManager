using BLL;
using BLL.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Logic
{
    public interface IDrinkService
    {
        IQueryable<Drink> SearchByName(string textToSearch, IQueryable<Drink> drinksListToSearch);

        IQueryable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch,
            IQueryable<Drink> drinksListToSearch, SearchDrinkOption searchOption);

        IQueryable<Drink> SearchByAlcoholContent(string alcoholicInfo, IQueryable<Drink> drinks,
            IQueryable<Drink> contemporaryList);
    }
}
