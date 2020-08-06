using System;

namespace BLL
{
    public class ReviewService
    {
        public void EditReview(Drink drink, int score, string review)
        {
            drink.DrinkReview.ReviewScore = score;
            drink.DrinkReview.ReviewText = review;
            drink.DrinkReview.ReviewDate = DateTime.Now;
        }
        public DrinkReview AddReview(int score, string review)
        {
            return new DrinkReview
            {
                ReviewScore = score,
                ReviewText = review,
                ReviewDate = DateTime.Now
            };
        }
    }
}
