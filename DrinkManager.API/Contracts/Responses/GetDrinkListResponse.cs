using System.Collections.Generic;

namespace DrinkManager.API.Contracts.Responses
{
    public class GetDrinkListResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string AlcoholicInfo { get; set; }
        public string GlassType { get; set; }
        public string ImageUrl { get; set; }
        public List<DrinkListIngredient> Ingredients { get; set; }
        public int AverageReview { get; set; }
        public decimal Price { get; set; }


        public class DrinkListIngredient
        {
            public string Name { get; set; }
        }
    }
}
