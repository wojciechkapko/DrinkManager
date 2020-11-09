using System;

namespace BLL
{
    public class ReviewService
    {
        // public void EditReview(Drink drink, int score, string review)
        // {
        //     drink.DrinkReview.ReviewScore = score;
        //     drink.DrinkReview.ReviewText = review;
        //     drink.DrinkReview.ReviewDate = DateTime.Now;
        // }

        public DrinkReview AddReview(int score, string review)
        {
            return new DrinkReview
            {
                ReviewScore = score,
                ReviewText = review,
                ReviewDate = DateTime.Now
            };
        }

        // public List<Drink> ShowReviewed(List<Drink> drinks)
        // {
        //     var contemporaryList = new List<Drink>();
        //     foreach (var drink in drinks)
        //     {
        //         if (drink.IsReviewed)
        //         {
        //             contemporaryList.Add(drink);
        //         }
        //     }
        //     if (contemporaryList.Count == 0)
        //     {
        //         Console.WriteLine("You haven't reviewed any drinks yet.");
        //         Console.WriteLine("Press any key to go back to main menu.");
        //         Console.ReadKey();
        //     }
        //     return contemporaryList;
        // }
    }
}