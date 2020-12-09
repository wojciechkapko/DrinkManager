using System;
using System.Collections.Generic;
using System.Text;
using BLL.ReportDataModels;

namespace BLL
{
    public class UserReport
    {
        public string Username { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int LoginsCount { get; set; }
        public TimeSpan LastSeen { get; set; }
        public TheMostData MostVisitedDrink { get; set; }
        public DrinkData? RecentlyReviewedDrink { get; set; }
        public DrinkData? RecentlyFavouriteDrink { get; set; }
    }
}
