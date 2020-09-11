﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly DrinkAppContext _context;

        public DrinkRepository(DrinkAppContext context)
        {
            _context = context;
        }

        public IQueryable<Drink> GetAllDrinks()
        {
            return _context.Drinks.AsQueryable();
        }

        public ValueTask<Drink> GetDrinkById(string id)
        {
            return _context.Drinks.FindAsync(id);
        }

        public List<Ingredient> GetIngredientsFor(string id)
        {
            var test = _context.DrinkIngredients.Where(d => d.DrinkId == id).Select(di => di.IngredientId).ToList();
            return _context.Ingredients.Where(i => test.Contains(i.IngredientId)).ToList();
        }

        public Task<Drink> FindDrink(Expression<Func<Drink, bool>> predicate)
        {
            return _context.Drinks.FirstOrDefaultAsync(predicate);
        }

        public async Task AddDrink(Drink drink)
        {
            await _context.Drinks.AddAsync(drink);
        }

        public void Update(Drink drink)
        {
            _context.Drinks.Update(drink);
        }

        public void DeleteDrink(Drink drink)
        {
            _context.Drinks.Remove(drink);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}