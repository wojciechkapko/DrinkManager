using BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerConsole
{
    public class FavouritesService
    {
        public static List<Drink> Favourites { get; } = new List<Drink>();

        public static void AddDrink(Drink drink)
        {
            if (Favourites.Contains(drink))
            {
                Console.WriteLine("\nYou already have this drink on your favourites list.");
            }
            else
            {
                Favourites.Add(drink);
                Console.WriteLine("\nDrink added to favourites.");
            }
        }

        public static void Display()
        {
            if (Favourites.Any())
            {
                Console.WriteLine("\nYour favourite drinks:\n");
                PagingHandler.DivideDrinkListIntoPages(Favourites);
            }
            else
            {
                Console.WriteLine("\nYour favourites list is empty.\n");
                Console.WriteLine("\nPress any key to go back to the main menu.");
                Console.ReadKey();
            }
        }
    }
}