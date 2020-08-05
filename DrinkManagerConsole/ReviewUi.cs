using BLL;
using System;

namespace DrinkManagerConsole
{
    public class ReviewUi
    {
        public static void EditReview(Drink drink)
        {
            
            Console.Clear();
            SearchDrinkConsoleUi.WriteDrinkInfo(drink);

            Console.WriteLine("Please provide new score:");
            var newScore = Console.Read();
            Console.WriteLine("Please write new review:");
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------------------------");
            var newReview = Console.ReadLine();
            drink.DrinkReview.ReviewScore = newScore;
            drink.DrinkReview.ReviewText = newReview;
            drink.DrinkReview.ReviewDate = DateTime.Now;

            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }
    }
}
