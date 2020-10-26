using System.Collections.Generic;
using System.Linq;
using BLL.Enums;

namespace BLL.Services
{
    public interface IDrinkSearchService
    {
        IEnumerable<Drink> SearchByName(string textToSearch);

        IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption);

        IQueryable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics, List<Drink> drinks);
    }
}
