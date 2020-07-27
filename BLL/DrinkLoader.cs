using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace BLL
{
    public class DrinkLoader
    {
        public List<Drink> InitializeDrinksFromFile()
        {
            var path = Environment.CurrentDirectory + "/" + "drinks_source.json";
            var drinks = LoadFromFile(path);

            return drinks;
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
                throw new FileNotFoundException("File not found, maybe the path was incorrect?");
            }

            return newDrinks;
        }
    }
}