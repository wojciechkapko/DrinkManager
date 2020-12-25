using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class SearchDrink
    {
        public const string DrinkIsAlcoholic = "Alcoholic";
        public const string DrinkIsNonAlcoholic = "Non alcoholic";
        public const string DrinkIsOptionalAlcohol = "Optional alcohol";
        public static List<Drink> SearchByName(string textToSearch, List<Drink> drinksListToSearch)
        {
            return drinksListToSearch
                .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        public static List<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, List<Drink> drinksListToSearch, SearchDrinkOption searchOption)
        {
            var drinksFound = new List<Drink>();
            var ingredientsFound = new SortedSet<string>();

            if (searchOption == SearchDrinkOption.All)
            {
                foreach (var drink in drinksListToSearch)
                {
                    foreach (var drinkIngredient in drink.Ingredients)
                    {
                        if (drinkIngredient.Name == null)
                        {
                            continue;
                        }

                        foreach (var ingredient in ingredientsToSearch)
                        {
                            if (drinkIngredient.Name.Contains(ingredient, StringComparison.InvariantCultureIgnoreCase))
                            {
                                ingredientsFound.Add(ingredient);
                            }
                        }
                    }

                    if (ingredientsFound.SetEquals(ingredientsToSearch))
                    {
                        drinksFound.Add(drink);
                    }

                    ingredientsFound.Clear();
                }
            }
            else if (searchOption == SearchDrinkOption.Any)
            {
                foreach (var drink in drinksListToSearch)
                {
                    var nextDrink = false;

                    foreach (var drinkIngredient in drink.Ingredients)
                    {
                        if (drinkIngredient.Name == null)
                        {
                            continue;
                        }

                        foreach (var ingredient in ingredientsToSearch)
                        {
                            if (drinkIngredient.Name.Contains(ingredient, StringComparison.InvariantCultureIgnoreCase))
                            {
                                drinksFound.Add(drink);
                                nextDrink = true;
                                break;
                            }
                        }

                        if (nextDrink)
                        {
                            break;
                        }
                    }
                }
            }

            return drinksFound;
        }


        public static IQueryable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics, List<Drink> drinks)
        {
            var contemporaryList = new List<Drink>();
            if (alcoholics)
            {
                contemporaryList.AddRange(drinks.Where(x => x.AlcoholicInfo == DrinkIsAlcoholic).ToList());
            }

            if (nonAlcoholics)
            {
                contemporaryList.AddRange(drinks.Where(x => x.AlcoholicInfo == DrinkIsNonAlcoholic).ToList());
            }

            if (optionalAlcoholics)
            {
                contemporaryList.AddRange(drinks.Where(x => x.AlcoholicInfo == DrinkIsOptionalAlcohol).ToList());
            }
            return contemporaryList.AsQueryable();
        }
    }
}
