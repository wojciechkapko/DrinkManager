using BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DrinkManagerWeb.Data
{
    public static class Seeder
    {
        public static void Seedit(IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider
                .GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope
                .ServiceProvider.GetService<DrinkAppContext>();

            // Check if we can connect to the database
            if (!context.Database.CanConnect())
            {
                // Create the database
                context.Database.Migrate();
                Console.WriteLine("Database does not exist...Creating");
            }
            // Check if we have data in the database
            if (!context.Drinks.Any())
            {
                // Load drinks from json file
                List<Drink> drinks = new DrinkLoader().InitializeDrinksFromFile();
                // Add drinks to the database
                context.AddRange(drinks);
                context.SaveChanges();
                Console.WriteLine("Database is empty...Seeding data");
                Thread.Sleep(1000);
            }
        }
    }
}