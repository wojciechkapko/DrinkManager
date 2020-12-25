using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Seeder
    {
        public static void SeedData(DrinkAppContext context)
        {
            // Check if we can connect to the database
            if (!context.Database.CanConnect())
            {
                // Create the database
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
            }
            if (!context.Settings.Any())
            {

                var settings = new List<Setting>
                {
                    new Setting
                    {
                        Name = "Next report date",
                        Value = null,
                        DisallowManualChange = true,
                        Description = "Date and time when the next report will be sent."
                    },
                    new Setting
                    {
                        Name = "Last report date",
                        Value = null,
                        DisallowManualChange = true,
                        Description = "Date and time when the last report was sent."
                    },
                    new Setting
                    {
                        Name = "Report interval type",
                        Value = IntervalTypes.Days.ToString(),
                        Description = "Interval type: days or hours.",
                        FrontEndElementType = "select",
                        AvailableOptions = "Days,Hours"
                    },
                    new Setting
                    {
                        Name = "Report interval",
                        Value = "1",
                        Description = "How often to send the report."
                    },
                    new Setting
                    {
                        Name = "Report time",
                        Value = "00:00:00",
                        Description = "Time at which the report should be sent."
                    }
                };
                // Add settings to the database
                context.AddRange(settings);
                context.SaveChanges();
            }
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByEmailAsync(configuration["AppSettings:AdminUserEmail"]);
            if (user == null)
            {

                //initializing custom roles 
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { "Admin", "User" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                //creating a power user who will maintain the app
                var powerUser = new AppUser()
                {
                    UserName = configuration["AppSettings:AdminUserEmail"],
                    Email = configuration["AppSettings:AdminUserEmail"],
                };

                string userPassword = configuration["AppSettings:UserPassword"];

                var createPowerUser = await userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await userManager.AddToRoleAsync(powerUser, "Admin");
                }
            }
        }
    }
}