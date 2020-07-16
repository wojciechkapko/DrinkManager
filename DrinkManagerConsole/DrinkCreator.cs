using System;
using System.Collections.Generic;
using BLL;

namespace DrinkManagerConsole
{
    internal class DrinkCreator
    {
        private readonly List<Drink> _drinks;

        public DrinkCreator(List<Drink> drinks)
        {
            _drinks = drinks;
        }

        public void AddNewDrink()
        {
            var newDrink = CreateDrink();
            _drinks.Add(newDrink);
            Console.WriteLine($"\nNew drink {newDrink.Name} added.\n");
        }

        private Drink CreateDrink()
        {
            return new Drink()
            {
                Name = Utility.GetGenericData("Drink name: "),
                IsAlcoholic = Utility.GetAlcoholicInfo(),
                Category = Utility.GetGenericData("Category name: "),
                Ingredients = Utility.GetIngredients(),
                Instructions = Utility.GetGenericData("Instructions: ")
            };
        }
    }
}