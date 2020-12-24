using BLL;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinksViewModel
    {
        public PaginatedList<Drink> Drinks { get; set; }
        public bool Alcoholics { get; set; }
        public bool NonAlcoholics { get; set; }
        public bool OptionalAlcoholics { get; set; }
    }
}