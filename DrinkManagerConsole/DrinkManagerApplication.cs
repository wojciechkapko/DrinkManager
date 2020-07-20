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
                path = Directory.GetCurrentDirectory() + "\\drinks_source.json";
            }

            string drinksString = null;
            var isFileRead = false;
            List<Drink> drinksListDeserialized = null;

            do
            {
                try
                {
                    drinksString = File.ReadAllText(path);
                    drinksListDeserialized = JsonConvert.DeserializeObject<List<Drink>>(drinksString);
                    isFileRead = true;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(
                        "Drinks list was not found in default location. Please provide correct path to the file: ");
                    path = Console.ReadLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("Problem with input file - please check if the input data file in the application main directory is not corrupted.");
                }
            } while (!isFileRead);

            return drinksListDeserialized;
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
