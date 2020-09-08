using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public Task<List<Drink>> GetAllDrinks()
        {
            return _context.Drinks.ToListAsync();
        }

        public ValueTask<Drink> GetDrinkById(string id)
        {
            return _context.Drinks.FindAsync(id);
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