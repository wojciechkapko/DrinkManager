using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Data;
using BLL.Data.Repositories;
using BLL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BLL.Services
{
    public class DrinkSearchService : IDrinkSearchService
    {
        public const string DrinkIsAlcoholic = "Alcoholic";
        public const string DrinkIsNonAlcoholic = "Non alcoholic";
        public const string DrinkIsOptionalAlcohol = "Optional alcohol";
        private readonly IMemoryCache _memoryCache;
        private readonly IDrinkRepository _repository;
        //private readonly DrinkAppContext _context;

        public DrinkSearchService(/*DrinkAppContext context*/ DrinkRepository repository, MemoryCache memoryCache)
        {
            //_context = context;
            _repository = repository;
        }

        public IEnumerable<Drink> SearchByName(string textToSearch)
        {
            //return _context.Drinks.AsEnumerable()
            //    .Where(drink => drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase));
            return _repository.GetAllDrinks().Where(drink =>
                drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption)
        {
            var drinksFound = new List<Drink>();
            var ingredientsFound = new SortedSet<string>();
            //var drinks = _context.Drinks.Include(drink => drink.Ingredients);
            var drinks = _repository.GetAllDrinksAsQueryable().Include(drink => drink.Ingredients);

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
    }
}