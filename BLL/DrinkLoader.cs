using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BLL
{
    public static class DrinkLoader
    {
        public static List<Drink> AddDrinksFromFile(List<Drink> drinks = null)
        {
            List<Drink> newDrinks;
            string path;

            if (drinks == null)
            {
                path = Environment.CurrentDirectory + "/" + "drinks_source.json";
            }
            else
            {
                path = GetPath();
            }

            try
            {
                newDrinks = LoadFromFile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n{e.Message}\n");
                return null;
            }

            if (drinks == null)
            {
                return newDrinks;
            }
            else
            {
                drinks.AddRange(newDrinks);
                return drinks;
            }
        }

        private static List<Drink> LoadFromFile(string path)
        {
            var drinksString = File.ReadAllText(path);
            var jo = JObject.Parse(drinksString);
            var newDrinks = jo.SelectToken("drinks", false).ToObject<List<Drink>>();

            return newDrinks;
        }

        private static string GetPath()
        {
            Console.WriteLine("Please provide full path to the file with new drinks");
            var path = Console.ReadLine();
            return path;
        }
    }
}