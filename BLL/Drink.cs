using Newtonsoft.Json;
using System.Collections.Generic;

namespace BLL
{
    public class Drink
    {
        private List<Ingredient> _ingredients;

        [JsonProperty("idDrink")]
        public string Id { get; set; }

        [JsonProperty("strDrink")]
        public string Name { get; set; }

        [JsonProperty("strCategory")]
        public string Category { get; set; }

        [JsonProperty("strAlcoholic")]
        public string AlcoholicInfo { get; set; }

        [JsonProperty("strGlass")]
        public string GlassType { get; set; }

        [JsonProperty("strInstructions")]
        public string Instructions { get; set; }

        [JsonProperty("strIngredient1")]
        public string IngredientName1 { get; set; }

        [JsonProperty("strIngredient2")]
        public string IngredientName2 { get; set; }

        [JsonProperty("strIngredient3")]
        public string IngredientName3 { get; set; }

        [JsonProperty("strIngredient4")]
        public string IngredientName4 { get; set; }

        [JsonProperty("strIngredient5")]
        public string IngredientName5 { get; set; }

        [JsonProperty("strIngredient6")]
        public string IngredientName6 { get; set; }

        [JsonProperty("strIngredient7")]
        public string IngredientName7 { get; set; }

        [JsonProperty("strIngredient8")]
        public string IngredientName8 { get; set; }

        [JsonProperty("strIngredient9")]
        public string IngredientName9 { get; set; }

        [JsonProperty("strIngredient10")]
        public string IngredientName10 { get; set; }

        [JsonProperty("strMeasure1")]
        public string IngredientMeasure1 { get; set; }

        [JsonProperty("strMeasure2")]
        public string IngredientMeasure2 { get; set; }

        [JsonProperty("strMeasure3")]
        public string IngredientMeasure3 { get; set; }

        [JsonProperty("strMeasure4")]
        public string IngredientMeasure4 { get; set; }

        [JsonProperty("strMeasure5")]
        public string IngredientMeasure5 { get; set; }

        [JsonProperty("strMeasure6")]
        public string IngredientMeasure6 { get; set; }

        [JsonProperty("strMeasure7")]
        public string IngredientMeasure7 { get; set; }

        [JsonProperty("strMeasure8")]
        public string IngredientMeasure8 { get; set; }

        [JsonProperty("strMeasure9")]
        public string IngredientMeasure9 { get; set; }

        [JsonProperty("strMeasure10")]
        public string IngredientMeasure10 { get; set; }

        public DrinkReview DrinkReview { get; set; }

        public bool isReviewed {
            get
            {
                if (DrinkReview != null)
                {
                    return true;
                }
                return false;
            } }

        public List<Ingredient> Ingredients
        {
            get
            {
                if (_ingredients == null)
                {
                    return new List<Ingredient>
                    {
                        new Ingredient
                        {
                            Name = IngredientName1,
                            Amount = IngredientMeasure1
                        },
                        new Ingredient
                        {
                            Name = IngredientName2,
                            Amount = IngredientMeasure2
                        },
                        new Ingredient
                        {
                            Name = IngredientName3,
                            Amount = IngredientMeasure3
                        },
                        new Ingredient
                        {
                            Name = IngredientName4,
                            Amount = IngredientMeasure4
                        },
                        new Ingredient
                        {
                            Name = IngredientName5,
                            Amount = IngredientMeasure5
                        },
                        new Ingredient
                        {
                            Name = IngredientName6,
                            Amount = IngredientMeasure6
                        },
                        new Ingredient
                        {
                            Name = IngredientName7,
                            Amount = IngredientMeasure7
                        },
                        new Ingredient
                        {
                            Name = IngredientName8,
                            Amount = IngredientMeasure8
                        },
                        new Ingredient
                        {
                            Name = IngredientName9,
                            Amount = IngredientMeasure9
                        },
                        new Ingredient
                        {
                            Name = IngredientName10,
                            Amount = IngredientMeasure10
                        }
                    };
                }
                else
                {
                    return _ingredients;
                }
            }
            set => _ingredients = value;
        }
    }
}