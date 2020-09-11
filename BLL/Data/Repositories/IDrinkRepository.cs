﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public interface IDrinkRepository
    {
        IQueryable<Drink> GetAllDrinks();

        ValueTask<Drink> GetDrinkById(string id);

        public Task<Drink> FindDrink(Expression<Func<Drink, bool>> predicate);

        Task AddDrink(Drink drink);

        void Update(Drink drink);

        void DeleteDrink(Drink drink);

        Task<bool> SaveChanges();
        List<Ingredient> GetIngredientsFor(string id);
    }
}