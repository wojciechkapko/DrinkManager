using BLL;
using BLL.Data;
using BLL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Services
{
    public class DrinkSearchService : IDrinkSearchService
    {
        public const string DrinkIsAlcoholic = "Alcoholic";
        public const string DrinkIsNonAlcoholic = "Non alcoholic";
        public const string DrinkIsOptionalAlcohol = "Optional alcohol";
        private readonly DrinkAppContext _context;

        public DrinkSearchService(DrinkAppContext context)
        {
            _context = context;
        }

        public IEnumerable<Drink> SearchByName(string textToSearch)
        {
            return _context.Drinks.AsEnumerable()
                .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption)
        {
            var drinksFound = new List<Drink>();
            var ingredientsFound = new SortedSet<string>();
            var drinks = _context.Drinks.Include(drink => drink.Ingredients);

            switch (searchOption)
            {
                case SearchDrinkOption.All:
                {
                    foreach (var drink in drinks)
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
                    break;
                }
                case SearchDrinkOption.Any:
                {
                    foreach (var drink in drinks)
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
                    break;
                }
            }
            return drinksFound;
        }

        public IQueryable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics, List<Drink> drinks)
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


        public IEnumerable<Drink> SortDrinks(string sortOrder, IEnumerable<Drink> drinks)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    drinks = drinks.OrderByDescending(s => s.Name);
                    break;
                default:
                    drinks = drinks.OrderBy(s => s.Name);
                    break;
            }
            return drinks;
        }
    }
}