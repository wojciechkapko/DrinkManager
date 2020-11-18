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
                        Name = "next.report.date",
                        Value = null,
                        DisallowManualChange = true,
                        Description = "Date and time when the next report will be sent."
                    },
                    new Setting
                    {
                        Name = "last.report.date",
                        Value = null,
                        DisallowManualChange = true,
                        Description = "Date and time when the last report was sent."
                    },
                    new Setting
                    {
                        Name = "report.interval",
                        Value = "1",
                        Description = "How often to send the report (in days)."
                    },
                    new Setting
                    {
                        Name = "report.time",
                        Value = "00:00:00",
                        Description = "Time at which the report should be sent."
                    }
                };
                // Add settings to the database
                context.AddRange(settings);
                context.SaveChanges();
            }
        }
    }
}