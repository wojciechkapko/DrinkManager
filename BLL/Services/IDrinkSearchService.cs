using BLL.Enums;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public interface IDrinkSearchService
    {
        IEnumerable<Drink> SearchByName(string textToSearch);

        IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption);

        IEnumerable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics);
    }
}
