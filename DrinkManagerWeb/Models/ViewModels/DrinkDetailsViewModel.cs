using BLL;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class DrinkDetailsViewModel
    {
        public Drink Drink { get; set; }
        public bool IsFavourite { get; set; }
        public bool CanUserReview { get; set; }
    }
}