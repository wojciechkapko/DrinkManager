using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DrinkManagerConsole
{
    public class DrinkManagerApplication
    {
        public readonly List<Drink> DrinksListGlobal;

        public DrinkManagerApplication()
        {
            DrinksListGlobal = LoadFromFile();
        }

        private List<Drink> LoadFromFile(string path = null)
        {
            if (path == null)
            {
                path = GetCurrentDirectoryPath();
            }

            string drinksString = null;
            var isFileRead = false;

            do
            {
                try
                {
                    drinksString = File.ReadAllText(path);
                    isFileRead = true;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Drinks list was not found in default location. Please provide correct path to the file: ");
                    path = Console.ReadLine();
                }
            } while (!isFileRead);

            return JsonConvert.DeserializeObject<List<Drink>>(drinksString);
        }

        private string GetCurrentDirectoryPath()
        {
            return Directory.GetCurrentDirectory() + "\\drinks_source.json";
        }

        public void Start()
        {
            bool isAppRunning = true;
            do
            {
                // Menu();
                // Other
                var searchDrinkByName = new SearchDrinkByName(DrinksListGlobal);
                searchDrinkByName.SearchByName();

                Console.Write("Continue (y/n)? ");
                if (Console.ReadLine().ToLower() == "n")
                {
                    isAppRunning = false;
                }
            } while (isAppRunning);
        }
    }
}
