using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IngredientId { get; set; }

        public string DrinkId { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }
    }
}