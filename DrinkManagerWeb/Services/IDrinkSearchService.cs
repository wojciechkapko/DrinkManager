using BLL;
using BLL.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Services
{
    public interface IDrinkSearchService
    {
        IEnumerable<Drink> SearchByName(string textToSearch, IEnumerable<Drink> drinksListToSearch);

        IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch,
            IEnumerable<Drink> drinksListToSearch, SearchDrinkOption searchOption);

        IEnumerable<Drink> SearchByAlcoholContent(string alcoholicInfo, IEnumerable<Drink> drinks,
            IEnumerable<Drink> contemporaryList);

        IEnumerable<Drink> SortDrinks(string sortOrder, IEnumerable<Drink> drinks);
    }
}
