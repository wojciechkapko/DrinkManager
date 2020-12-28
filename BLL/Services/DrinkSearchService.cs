﻿using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class DrinkSearchService : IDrinkSearchService
    {
        public const string DrinkIsAlcoholic = "Alcoholic";
        public const string DrinkIsNonAlcoholic = "Non alcoholic";
        public const string DrinkIsOptionalAlcohol = "Optional alcohol";
        private readonly IDrinkRepository _repository;

        public DrinkSearchService(IDrinkRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Drink> SearchByName(string textToSearch)
        {
            var drink = _repository.GetAllDrinks().Where(x => x.Name.ToLower().Contains(textToSearch.ToLower()));
            return drink;
        }

        public IQueryable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption)
        {
            var drinksFound = new List<Drink>();
            var ingredientsFound = new SortedSet<string>();
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
            return drinksFound.AsQueryable();
        }

        public IQueryable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics)
        {
            var drinks = _repository.GetAllDrinksAsQueryable();
            var contemporaryList = new List<Drink>();
            if (alcoholics)
            {
                contemporaryList.AddRange(drinks.Where(x => x.AlcoholicInfo == DrinkIsAlcoholic));
            }

            if (nonAlcoholics)
            {
                contemporaryList.AddRange(drinks.Where(x => x.AlcoholicInfo == DrinkIsNonAlcoholic));
            }

            if (optionalAlcoholics)
            {
                contemporaryList.AddRange(drinks.Where(x => x.AlcoholicInfo == DrinkIsOptionalAlcohol));
            }
            return contemporaryList.AsQueryable();
        }
    }
}