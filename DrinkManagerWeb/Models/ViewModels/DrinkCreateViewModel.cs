using BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinkCreateViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string Category { get; set; }
        public string AlcoholicInfo { get; set; }
        [StringLength(25)]
        public string GlassType { get; set; }
        [Required]
        public string Instructions { get; set; }
        [DisplayName("Image URL")]
        [Url]
        public string ImageUrl { get; set; }
        [DisplayName("Ingredient")]
        [Required(ErrorMessage = "First ingredient is required.")]
        [StringLength(25)]
        [RegularExpression(".*\\s.*", ErrorMessage = "Amount separated by space is required.")]
        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; }
        public string Ingredient3 { get; set; }
        public string Ingredient4 { get; set; }
        public string Ingredient5 { get; set; }
        public string Ingredient6 { get; set; }
        public string Ingredient7 { get; set; }
        public string Ingredient8 { get; set; }
        public string Ingredient9 { get; set; }
        public string Ingredient10 { get; set; }
        public string Ingredient11 { get; set; }
        public string Ingredient12 { get; set; }
        public string Ingredient13 { get; set; }
        public string Ingredient14 { get; set; }
        public string Ingredient15 { get; set; }


        public List<Ingredient> Ingredients { get; set; }
    }
}
