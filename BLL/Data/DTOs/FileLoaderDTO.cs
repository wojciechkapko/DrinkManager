using System.Collections.Generic;

namespace BLL.Data.DTOs
{
    public class FileLoaderDto
    {
        public List<Drink> Drinks { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<DrinkIngredient> DrinkIngredients { get; set; }
    }
}
