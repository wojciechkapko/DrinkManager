using BLL;
using BLL.Enums;
using System.Collections.Generic;

namespace DrinkManagerWeb.Services
{
    public interface IDrinkSearchService
    {
        IEnumerable<Drink> SearchByName(string textToSearch);

        IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption);

        IEnumerable<Drink> SearchByAlcoholContent(string alcoholicInfo, IEnumerable<Drink> drinks,
            IEnumerable<Drink> contemporaryList);

        IEnumerable<Drink> SortDrinks(string sortOrder, IEnumerable<Drink> drinks);
    }
}
