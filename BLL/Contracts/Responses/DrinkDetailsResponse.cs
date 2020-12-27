using System.Collections.Generic;

namespace BLL.Contracts.Responses
{
    public class DrinkDetailsResponse
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string AlcoholicInfo { get; set; }
        public string GlassType { get; set; }
        public string ImageUrl { get; set; }
        public string Instructions { get; set; }
        public List<DrinkDetailsIngredient> Ingredients { get; set; }
        public int AverageReview { get; set; }
        public decimal Price { get; set; }

        public class DrinkDetailsIngredient
        {
            public string Name { get; set; }
            public string Amount { get; set; }
        }
    }
}