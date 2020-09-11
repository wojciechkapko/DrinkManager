using Microsoft.EntityFrameworkCore;

namespace BLL.Data
{
    public class DrinkAppContext : DbContext
    {
        public DrinkAppContext(DbContextOptions<DrinkAppContext> options) : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DrinkIngredient> DrinkIngredients { get; set; }
        public DbSet<DrinkReview> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkIngredient>()
                .HasKey(t => new { t.DrinkId, t.IngredientId });

            modelBuilder.Entity<DrinkIngredient>()
                .HasOne(pt => pt.Drink)
                .WithMany(p => p.DrinkIngredients)
                .HasForeignKey(pt => pt.DrinkId);

            modelBuilder.Entity<DrinkIngredient>()
                .HasOne(pt => pt.Ingredient)
                .WithMany(t => t.DrinkIngredients)
                .HasForeignKey(pt => pt.IngredientId);
        }
    }
}