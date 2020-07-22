using System.Collections.Generic;

namespace BLL
{
    public class SearchDrinkByName
    {
        private readonly List<Drink> _drinksListToSearch;

        public SearchDrinkByName(List<Drink> drinksList)
        {
            _drinksListToSearch = drinksList;
        }

        public List<Drink> SearchByName(string textToSearch)
        {
            var drinksFound = new List<Drink>();

            foreach (var drink in _drinksListToSearch)
            {
                if (drink.Name.ToLower().Contains(textToSearch.ToLower()))
                {
                    drinksFound.Add(drink);
                }
            }

            return drinksFound;
        }
    }
}
