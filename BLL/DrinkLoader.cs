using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BLL
{
    public class DrinkLoader
    {
        public List<Drink> InitializeDrinksFromFile()
        {
            var path = Environment.CurrentDirectory + "/" + "drinks_source.json";
            var newDrinks = LoadFromFile(path);

            return newDrinks;
        }

        public void AddDrinksFromFile(List<Drink> currentDrinks, string path)
        {
            var newDrinks = LoadFromFile(path);
            if (newDrinks != null)
            {
                currentDrinks.AddRange(newDrinks);
            }
        }

        private List<Drink> LoadFromFile(string path)
        {
            List<Drink> newDrinks;
            try
            {
                var drinksString = File.ReadAllText(path);
                var jo = JObject.Parse(drinksString);
                newDrinks = jo.SelectToken("drinks", false).ToObject<List<Drink>>();
            }
            catch (Exception)
            {
                Console.WriteLine($"\nOperation failed, path was incorrect\n");
                return null;
            }

            return newDrinks;
        }
    }
}