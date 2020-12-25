﻿using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DrinkAppContext : IdentityDbContext<AppUser>
    {
        public DrinkAppContext(DbContextOptions<DrinkAppContext> options) : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DrinkReview> Reviews { get; set; }
        public DbSet<UserDrink> UserDrinks { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drink>().Property(drink => drink.AverageReview).ValueGeneratedOnUpdate();

            modelBuilder.Entity<UserDrink>()
                .HasKey(cs => new { cs.AppUserId, cs.DrinkId });

            // modelBuilder.Entity<Drink>().HasMany(d => d.DrinkReviews).WithOne()
            //     .OnDelete(DeleteBehavior.Cascade);
            // modelBuilder.Entity<AppUser>().HasMany(d => d.Reviews).WithOne()
            //     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DrinkReview>()
                .HasOne(d => d.Drink)
                .WithMany(d => d.DrinkReviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DrinkReview>()
                .HasOne(d => d.Author)
                .WithMany(d => d.Reviews)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}