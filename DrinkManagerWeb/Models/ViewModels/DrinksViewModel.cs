using BLL;
using System.Collections.Generic;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinksViewModel
    {
        public PaginatedList<Drink> Drinks { get; set; }
    }
}