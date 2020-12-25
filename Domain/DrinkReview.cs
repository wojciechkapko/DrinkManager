﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class DrinkReview
    {
        public const int MinScore = 0;
        public const int MaxScore = 5;

        [Key]
        public int Id { get; set; }

        public int ReviewScore { get; set; }

        public string ReviewText { get; set; }

        public DateTime ReviewDate { get; set; }

        public Drink Drink { get; set; }
        public AppUser Author { get; set; }
    }
}