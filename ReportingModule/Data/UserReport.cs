using ReportingModuleApi.ReportDataModels;

namespace ReportingModuleApi.Data
{
    public class UserReport
    {
        public string Username { get; set; }
        public string RegisteredAt { get; set; }
        public int LoginsCount { get; set; }
        public LastSeenData LastSeen { get; set; }
        public TheMostData MostVisitedDrink { get; set; }
        public DrinkData RecentlyReviewedDrink { get; set; }
        public DrinkData RecentlyFavouriteDrink { get; set; }
    }
}
