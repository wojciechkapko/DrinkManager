using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinkCreateViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string Category { get; set; }
        public string AlcoholicInfo { get; set; }
        [StringLength(15)]
        public string GlassType { get; set; }
        [Required]
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Ingredient")]
        [Required(ErrorMessage = "First ingredient is required.")]
        [StringLength(15)]
        [RegularExpression(".*\\s.*", ErrorMessage = "Amount separated by space is required.")]
        public string Ingredient1 { get; set; }
    }
}
