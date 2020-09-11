using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IngredientId { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }

        public List<DrinkIngredient> DrinkIngredients { get; set; }
    }
}