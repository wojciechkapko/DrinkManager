using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BLL
{
    public class DrinkLoader
    {
        public List<Drink> InitializeDrinksFromFile()
        {
            var path = Environment.CurrentDirectory + "/" + "drinks_source.json";
            var data = LoadFromFile(path);

            return data;
        }

        public List<Drink> LoadFromFile(string path)
        {
            var newDrinks = new List<Drink>();
            var newIngredients = new List<Ingredient>();

            try
            {
                var drinksString = File.ReadAllText(path);
                var jo = JObject.Parse(drinksString);
                dynamic deserializedJson = JsonConvert.DeserializeObject(jo.SelectToken("drinks", false).ToString());

                foreach (var drink in deserializedJson)
                {
                    // create new drink
                    var newDrink = new Drink
                    {
                        Name = drink.strDrink,
                        AlcoholicInfo = drink.strAlcoholic,
                        Category = drink.strCategory,
                        GlassType = drink.strGlass,
                        Instructions = drink.strInstructions,
                        ImageUrl = drink.strDrinkThumb
                    };


                    // create current drink's ingredients list
                    var ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            Name = drink.strIngredient1,
                            Amount = (drink.strIngredient1 != null && drink.strMeasure1 == null ) ? " " : drink.strMeasure1
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient2,
                            Amount = (drink.strIngredient2 != null && drink.strMeasure2 == null ) ? " " : drink.strMeasure2
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient3,
                            Amount = (drink.strIngredient3 != null && drink.strMeasure3 == null ) ? " " : drink.strMeasure3
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient4,
                            Amount = (drink.strIngredient4 != null && drink.strMeasure4 == null ) ? " " : drink.strMeasure4
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient5,
                            Amount = (drink.strIngredient5 != null && drink.strMeasure5 == null ) ? " " : drink.strMeasure5
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient6,
                            Amount = (drink.strIngredient6 != null && drink.strMeasure6 == null ) ? " " : drink.strMeasure6
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient7,
                            Amount = (drink.strIngredient7 != null && drink.strMeasure7 == null ) ? " " : drink.strMeasure7
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient8,
                            Amount = (drink.strIngredient8 != null && drink.strMeasure8 == null ) ? " " : drink.strMeasure8
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient9,
                            Amount = (drink.strIngredient9 != null && drink.strMeasure9 == null ) ? " " : drink.strMeasure9
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient10,
                            Amount = (drink.strIngredient10 != null && drink.strMeasure10 == null ) ? " " : drink.strMeasure10
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient11,
                            Amount = (drink.strIngredient11 != null && drink.strMeasure11 == null ) ? " " : drink.strMeasure11
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient12,
                            Amount = (drink.strIngredient12 != null && drink.strMeasure12 == null ) ? " " : drink.strMeasure12
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient13,
                            Amount = (drink.strIngredient13 != null && drink.strMeasure13 == null ) ? " " : drink.strMeasure13
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient14,
                            Amount = (drink.strIngredient14 != null && drink.strMeasure14 == null ) ? " " : drink.strMeasure14
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient15,
                            Amount = (drink.strIngredient15 != null && drink.strMeasure15 == null ) ? " " : drink.strMeasure15
                        }
                    };
                    // remove null ingredients
                    ingredients = ingredients.Where(x => x.Name != null).ToList();
                    // add current drink's ingredients to all ingredients list
                    newDrink.Ingredients = ingredients;
                    // add new drink to all drinks list
                    newDrinks.Add(newDrink);
                }
            }
            catch (Exception)
            {
                throw new FileNotFoundException("File not found, maybe the path was incorrect?");
            }

            return newDrinks;
        }
    }
}