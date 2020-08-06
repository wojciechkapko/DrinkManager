using System;

namespace BLL
{
    public class ReviewService
    {
        public bool EditReview(Drink drink, int score, string review)
        {
            if (!drink.isReviewed)
            {
                return false;
            }

            drink.DrinkReview.ReviewScore = score;
            drink.DrinkReview.ReviewText = review;
            drink.DrinkReview.ReviewDate = DateTime.Now;
            return true;
        }
    }
}
