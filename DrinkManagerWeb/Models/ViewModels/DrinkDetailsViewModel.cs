using BLL;
using System.Collections.Generic;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinkDetailsViewModel
    {
        public Drink Drink { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}