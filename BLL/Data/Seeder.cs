using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BLL.Data
{
    public static class Seeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope
                .ServiceProvider.GetService<DrinkAppContext>();

            // Check if we can connect to the database
            if (!context.Database.CanConnect())
            {
                // Create the database
                Console.WriteLine("Database does not exist...Creating");
                context.Database.Migrate();
            }
            // Check if we have data in the database
            if (!context.Drinks.Any())
            {
                // Load drinks from json file
                var data = new DrinkLoader().InitializeDrinksFromFile();
                // Add drinks to the database
                context.AddRange(data.Drinks);
                context.SaveChanges();
                Console.WriteLine("Database is empty...Seeding data");
            }
        }
    }
}