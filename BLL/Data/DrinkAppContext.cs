using BLL.Admin.Models;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Data
{
    public class DrinkAppContext : IdentityDbContext
    {
        public DrinkAppContext(DbContextOptions<DrinkAppContext> options) : base(options)
        {
        }

        public new DbSet<AppUser> Users { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DrinkReview> Reviews { get; set; }
        public DbSet<UserDrink> UserDrinks { get; set; }
        public DbSet<Setting> Settings { get; set; }


        public override Int32 SaveChanges()
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess: true);
        }
        public override Int32 SaveChanges(Boolean acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithTriggers(base.SaveChanges, acceptAllChangesOnSuccess);
        }
        public override Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);
        }
        public override Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.SaveChangesWithTriggersAsync(base.SaveChangesAsync, acceptAllChangesOnSuccess, cancellationToken);
        }

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