using System;
using System.Collections.Generic;
using BLL;

namespace DrinkManagerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var drinks = new List<Drink>();

            var creator = new DrinkCreator(drinks);
            creator.AddNewDrink();

            foreach (var drink in drinks)
            {
                Console.WriteLine(drink.Name);
                Console.WriteLine(drink.Instructions);

                foreach (var drinkIngredient in drink.Ingredients)
                {
                    Console.WriteLine($"{drinkIngredient.Name} {drinkIngredient.Amount}");
                }
            }
        }
    }
}