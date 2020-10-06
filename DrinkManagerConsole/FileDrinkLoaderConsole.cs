using System.Collections.Generic;

namespace BLL
{
    public class FileDrinkLoaderConsole
    {

        public void AddDrinksFromFile(List<Drink> currentDrinks, string path)
        {

            var newDrinks = new DrinkLoader().LoadFromFile(path);
            if (newDrinks != null)
            {
                currentDrinks.AddRange(newDrinks);
            }
        }
    }
}