using BLL.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Data
{
    public static class Seeder
    {
        public static void SeedData(DrinkAppContext context)
        {
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
                context.AddRange(data);
                context.SaveChanges();
                Console.WriteLine("Database is empty...Seeding data");
            }
            if (!context.Settings.Any())
            {

                var settings = new List<Setting>
                {
                    new Setting
                    {
                        Name = "report.time",
                        Value = "00:00:00"
                    }
                };
                // Add settings to the database
                context.AddRange(settings);
                context.SaveChanges();
                Console.WriteLine("Database is empty...Seeding data");
            }
        }
    }
}