using BLL;
using System;

namespace DrinkManagerConsole
{
    public static class ReviewUi
    {
        public static void EditReview(Drink drink)
        {
            Console.Clear();
            Console.WriteLine("Current drink information:");
            SearchDrinkConsoleUi.WriteDrinkInfo(drink);

            int newScore;
            do
            {
                Console.WriteLine("\nPlease provide new score (0 - 5):");
            } while (!int.TryParse(Console.ReadLine(), out newScore) && newScore >= 0 && newScore <= 5);
           
            string newReview;
            do
            {
                Console.WriteLine("\nPlease write new review:");
                newReview = Console.ReadLine();
            } while (newReview == null);

            ReviewService reviewService = new ReviewService();
            if (reviewService.EditReview(drink, newScore, newReview))
            {
                Console.WriteLine("\nYour new review has been saved.");
                Console.WriteLine("\nNew drink information:");
                SearchDrinkConsoleUi.WriteDrinkInfo(drink);
            }
            else
            {
                Console.WriteLine("\nError occured - please try again.");
            }

            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }
    }
}
