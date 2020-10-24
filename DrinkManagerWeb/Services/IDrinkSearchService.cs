using BLL;
using BLL.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Services
{
    public interface IDrinkSearchService
    {
        IEnumerable<Drink> SearchByName(string textToSearch);

        IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption);

        IQueryable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics, List<Drink> drinks);
    }
}
