﻿using Microsoft.EntityFrameworkCore;
using System;
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
            return _context.Drinks.Include(i => i.Ingredients).OrderBy(x => x.Name);
        }

        public IQueryable<Drink> GetAllDrinksAsQueryable()
        {
            return _context.Drinks.Include(i => i.DrinkReviews).AsQueryable();
        }

        public Task<Drink> GetDrinkById(string id)
        {
            return _context.Drinks.Include(i => i.Ingredients).Include(i => i.DrinkReviews).ThenInclude(r => r.Author)
                .FirstOrDefaultAsync(d => d.DrinkId.Equals(id));
        }

        public Task<Drink> FindDrink(Expression<Func<Drink, bool>> predicate)
        {
            return _context.Drinks.Include(i => i.Ingredients).FirstOrDefaultAsync(predicate);
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