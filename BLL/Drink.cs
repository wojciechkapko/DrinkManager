using System.Collections.Generic;

namespace BLL
{
    public class Drink
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string AlcoholicInfo { get; set; }
        public string GlassType { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public DrinkReview DrinkReview { get; set; }
        public bool isReviewed
        {
            get
            {
                if (DrinkReview != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}