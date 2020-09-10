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
            var drinks = LoadFromFile(path);

            return drinks;
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
            try
            {
                var drinksString = File.ReadAllText(path);
                var jo = JObject.Parse(drinksString);
                dynamic deserializedJson = JsonConvert.DeserializeObject(jo.SelectToken("drinks", false).ToString());

                foreach (var drink in deserializedJson)
                {
                    newDrinks.Add(new Drink
                    {
                        Id = drink.idDrink,
                        Name = drink.strDrink,
                        AlcoholicInfo = drink.strAlcoholic,
                        Category = drink.strCategory,
                        GlassType = drink.strGlass,
                        Instructions = drink.strInstructions,
                        ImageUrl = drink.strDrinkThumb,
                        Ingredients = new List<Ingredient>
                        {
                            new Ingredient
                            {
                                Name = drink.strIngredient1,
                                Amount = drink.strMeasure1
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient2,
                                Amount = drink.strMeasure2
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient3,
                                Amount = drink.strMeasure3
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient4,
                                Amount = drink.strMeasure4
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient5,
                                Amount = drink.strMeasure5
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient6,
                                Amount = drink.strMeasure6
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient7,
                                Amount = drink.strMeasure7
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient8,
                                Amount = drink.strMeasure8
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient9,
                                Amount = drink.strMeasure9
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient10,
                                Amount = drink.strMeasure10
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient11,
                                Amount = drink.strMeasure11
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient12,
                                Amount = drink.strMeasure12
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient13,
                                Amount = drink.strMeasure13
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient14,
                                Amount = drink.strMeasure14
                            },
                            new Ingredient
                            {
                                Name = drink.strIngredient15,
                                Amount = drink.strMeasure15
                            }
                        }
                    });
                }

                foreach (var drink in newDrinks)
                {
                    drink.Ingredients = drink.Ingredients.Where(x => x.Name != null).ToList();
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