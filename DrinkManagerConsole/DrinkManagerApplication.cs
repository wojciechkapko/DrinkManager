using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BLL;
using Newtonsoft.Json;


namespace DrinkManagerConsole
{
    internal class DrinkManagerApplication
    {
        internal readonly List<Drink> drinksListGlobal;

        public DrinkManagerApplication()
        {
            drinksListGlobal = LoadFromFile();
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
    }
}
