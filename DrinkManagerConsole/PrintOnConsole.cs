using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace DrinkManagerConsole
{
    public class PrintOnConsole
    {
        public static void PrintDrinksOnPage(List<Drink> contemporaryList, int counter, int index)
        {
            Console.WriteLine($"{counter}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(index).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(index).AlcoholicInfo.PadRight(12, ' '));
        }
        public static void PrintInstructionWhileOnPagedList(List<Drink> contemporaryList, int page)
        {
            if (page * 9 + 9 > contemporaryList.Count)
            {
                if (page == 0)
                {
                    Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go back to main menu, press ESC");
                }
                else
                {
                    Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go to previous page press P\nIf you want to go back to main menu, press ESC");
                }
            }
            else if (page == 0)
            {
                Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go to next page press N\nIf you want to go back to main menu, press ESC");
            }
            else
            {
                Console.WriteLine("\nIf you want to check any drink, press its corresponding number\nIf you want to go to next page press N, or P to go to next to previous page\nIf you want to go back to main menu, press ESC");
            }
        }
        public static void TellUserThatHeCanNotGoToNextPage(int page)
        {
            if (page == 0)
            {
                Console.WriteLine("\nThis is the only page, there is no more pages");
            }
            else
            {
                Console.WriteLine("\nThis is the last page, there is no more");
            }
        }
        public static void TellUserThatHeCanNotGoBack(List<Drink> contemporaryList, int page)
        {
            if (page * 9 + 9 > contemporaryList.Count)
            {
                Console.WriteLine("\nThis is the one and only page");
            }
            else
            {
                Console.WriteLine("\nThis is the first page");
            }
        }
        public static void WriteExceptionCaughtInfo()
        {
            Console.WriteLine("Exception caught: ArgumentOutOfRangeException. Next time choose the number that is on the list!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        public static void PressAnyKeyToGoBackToPreviousMenu()
        {
            Console.WriteLine("\nTo go back to previous menu, press any key");
            Console.ReadKey();
        }
        public static void ReWriteDrinkListOnConsole(List<Drink> contemporaryList, int page, ConsoleKeyInfo choice)
        {
            Console.Clear();
            //Print next page of drinks if user pressed key for Next Page
            if (choice.Key == ConsoleKey.N)
            {
                for (int i = page * 9; i < contemporaryList.Count; i++)
                {
                    Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                }
            }
            //Print previous page of drinks if user pressed key for Previous Page
            else if (choice.Key == ConsoleKey.P)
            {
                for (int i = page * 9; i < Math.Min(9, contemporaryList.Count); i++)
                {
                    Console.WriteLine($"{i + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                }
            }
            //Reprints list after drink check or pressing unsupported input
            else
            {
                for (int i = page * 9; i < Math.Min(page * 9 + 9, contemporaryList.Count); i++)
                {
                    Console.WriteLine($"{i - page * 9 + 1}.".PadRight(6, ' ') + contemporaryList.ElementAt<Drink>(i).Name.PadRight(16, ' ') + contemporaryList.ElementAt<Drink>(i).AlcoholicInfo.PadRight(12, ' '));
                }
            }
        }
        public static void WriteDrinkInfo(Drink drink)
        {
            Console.WriteLine(
                         "\n------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Name:".PadRight(20) + drink.Name.PadRight(30) + drink.AlcoholicInfo);
            Console.WriteLine("Category:".PadRight(20) + drink.Category);
            Console.WriteLine("Glass type:".PadRight(20) + drink.GlassType);
            Console.WriteLine("\nIngredients: ");
            foreach (var ingredient in drink.Ingredients)
            {
                if (ingredient.Name == null)
                {
                    continue;
                }

                Console.WriteLine(ingredient.Name.PadRight(20) + ingredient.Amount);
            }

            Console.WriteLine($"\nInstructions:\n{drink.Instructions}");
            Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------------");
            return;
        }
    }
}