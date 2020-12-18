//using BLL;
//using System;

//namespace DrinkManagerConsole
//{
//    public static class ReviewUi
//    {
//        public static void GetDataFromUserForNewReview(Drink drink)
//        {
//            Console.Clear();
//            Console.WriteLine("Current drink information:");
//            SearchDrinkConsoleUi.WriteDrinkInfo(drink);

//            int newScore;
//            do
//            {
//                Console.WriteLine($"\nPlease provide new score ({DrinkReview.MinScore} - {DrinkReview.MaxScore}):");
//            } while (!int.TryParse(Console.ReadLine(), out newScore) || newScore < DrinkReview.MinScore || newScore > DrinkReview.MaxScore);

//            string newReview;
//            do
//            {
//                Console.WriteLine("\nPlease write new review:");
//                newReview = Console.ReadLine();
//            } while (newReview == null);

//            ReviewService reviewService = new ReviewService();
//            if (drink.IsReviewed)
//            {
//                reviewService.EditReview(drink, newScore, newReview);
//                Console.WriteLine("\nYour new review has been saved.");
//                Console.WriteLine("\nNew drink information:");
//                SearchDrinkConsoleUi.WriteDrinkInfo(drink);
//            }
//            else
//            {
//                drink.DrinkReview = reviewService.AddReview(newScore, newReview);
//                Console.WriteLine("\nThank you for reviewing our drink!");
//                Console.WriteLine("\nNew drink information:");
//                SearchDrinkConsoleUi.WriteDrinkInfo(drink);
//            }
//        }
//    }
//}