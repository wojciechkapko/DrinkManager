﻿using BLL;
using Microsoft.EntityFrameworkCore;

namespace DrinkManagerWeb.Data
{
    public class DrinkAppContext : DbContext
    {
        public DrinkAppContext(DbContextOptions<DrinkAppContext> options) : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DrinkReview> Reviews { get; set; }
    }
}