using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BLL
{
    public class DrinkLoader
    {
        private List<Drink> _drinks;

        public DrinkLoader(List<Drink> drinks)
        {
            _drinks = drinks;
        }

        public void AddDrinksFromFile()
        {
            var newDrinks = new List<Drink>();
            var path = Environment.CurrentDirectory + "/" + "drinks_source.json";

            if (File.Exists(path) == false)
            {
                path = GetPath();
            }

            try
            {
                newDrinks = LoadFromFile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();
                return;
            }

            if (_drinks != null)
            {
                _drinks.AddRange(newDrinks);
            }
            else
            {
                _drinks = newDrinks;
            }
        }

        private List<Drink> LoadFromFile(string path)
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