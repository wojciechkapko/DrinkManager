using Domain;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Seeder
    {
        public static async Task SeedData(
            DrinkAppContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
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


            if (!roleManager.Roles.Any())
            {
                string[] roleNames = { "Manager", "Employee" };

                foreach (var roleName in roleNames)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        UserName = configuration["AdminEmail"],
                        Email = configuration["AdminEmail"]
                    },
                    new AppUser
                    {
                        UserName = "TestUser",
                        Email = "Testuser@test.com"
                    },
                    new AppUser
                    {
                        UserName = "TestUser2",
                        Email = "Testuser2@test.com"
                    }
                };

                var adminPassword = configuration["AdminPassword"];
                var admin = await userManager.CreateAsync(users[0], adminPassword);
                if (admin.Succeeded)
                {
                    await userManager.AddToRoleAsync(users[0], "Manager");
                }

                foreach (var appUser in users.Skip(1))
                {
                    var user = await userManager.CreateAsync(appUser, adminPassword);
                    if (user.Succeeded)
                    {
                        await userManager.AddToRoleAsync(appUser, "Employee");
                    }
                }
            }
        }
    }
}