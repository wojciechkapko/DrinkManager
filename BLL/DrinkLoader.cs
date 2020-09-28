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

        public void AddDrinksFromFile(List<Drink> currentDrinks, string path)
        {
            var newDrinks = LoadFromFile(path);
            if (newDrinks != null)
            {
                currentDrinks.AddRange(newDrinks);
            }
        }

        private List<Drink> LoadFromFile(string path)
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
                        DrinkId = drink.idDrink.ToString(),
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
                            Amount = drink.strMeasure1,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient2,
                            Amount = drink.strMeasure2,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient3,
                            Amount = drink.strMeasure3,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient4,
                            Amount = drink.strMeasure4,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient5,
                            Amount = drink.strMeasure5,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient6,
                            Amount = drink.strMeasure6,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient7,
                            Amount = drink.strMeasure7,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient8,
                            Amount = drink.strMeasure8,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient9,
                            Amount = drink.strMeasure9,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient10,
                            Amount = drink.strMeasure10,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient11,
                            Amount = drink.strMeasure11,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient12,
                            Amount = drink.strMeasure12,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient13,
                            Amount = drink.strMeasure13,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient14,
                            Amount = drink.strMeasure14,
                            IngredientId = Guid.NewGuid().ToString()
                        },
                        new Ingredient
                        {
                            Name = drink.strIngredient15,
                            Amount = drink.strMeasure15,
                            IngredientId = Guid.NewGuid().ToString()
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