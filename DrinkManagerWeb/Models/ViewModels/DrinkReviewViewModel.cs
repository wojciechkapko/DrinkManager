using System.ComponentModel.DataAnnotations;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinkReviewViewModel
    {
        
        [Required]
        [StringLength(150)]
        public string ReviewText { get; set; }

        [Required]
        public int ReviewScore { get; set; }

    }
}