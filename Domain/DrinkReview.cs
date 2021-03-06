﻿using System;

namespace Domain
{
    public class DrinkReview
    {
        public const int MinScore = 0;
        public const int MaxScore = 5;

        public int Id { get; set; }

        public string DrinkId { get; set; }

        public int ReviewScore { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }

        public string AuthorName { get; set; }
        public Drink Drink { get; set; }
    }
}