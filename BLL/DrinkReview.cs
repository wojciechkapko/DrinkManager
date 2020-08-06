using System;

namespace BLL
{
    public class DrinkReview
    {
        public const int MinScore = 0;
        public const int MaxScore = 5;

        public int ReviewScore { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
