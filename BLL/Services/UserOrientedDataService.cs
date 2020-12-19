using BLL.Data.Repositories;
using BLL.Enums;
using BLL.ReportDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserOrientedDataService : IUserOrientedDataService
    {
        private readonly IActivitiesRepository _activitiesRepository;
        public UserOrientedDataService(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }
        public async Task<string> GetUserCreationDate(string username)
        {
            var activities = await _activitiesRepository.Get(x =>
                x.Username == username);
            if (activities.Count.Equals(0))
            {
                return "-";
            }
            return activities.OrderByDescending(x => x.Created).First().Created.ToString("u");
        }

        public async Task<int> GetUserLoginsCount(string username)
        {
            var activities = await _activitiesRepository.Get(x =>
                x.Username == username && (x.Action == PerformedAction.ExternalLogin ||
                x.Action == PerformedAction.SuccessfulLogin));
            if (activities.Count.Equals(0))
            {
                return 0;
            }
            return activities.Count;
        }
        public async Task<LastSeenData> GetTimeSinceLastUserActivity(string username)
        {
            var activities = await _activitiesRepository.Get(x => x.Username == username);
            var lastSeen = new LastSeenData();
            if (activities.Count.Equals(0))
            {
                lastSeen.Days = 0;
                lastSeen.Hours = 0;
                lastSeen.Minutes = 0;
                lastSeen.Seconds = 0;
            }
            else
            {
                var timeOfLastActivity = DateTime.Now - activities.First().Created;
                lastSeen.Days = timeOfLastActivity.Days;
                lastSeen.Hours = timeOfLastActivity.Hours;
                lastSeen.Minutes = timeOfLastActivity.Minutes;
                lastSeen.Seconds = timeOfLastActivity.Seconds;
            }

            return lastSeen;
        }
        public async Task<TheMostData> GetMostVisitedDrinkData(string username)
        {
            var activities =
                await _activitiesRepository.Get(x => x.Username == username && x.Action == PerformedAction.VisitedDrink);
            var mostVisitedDrink = new TheMostData();
            if (activities.Count.Equals(0))
            {
                mostVisitedDrink.Count = 0;
                mostVisitedDrink.Name = "none";
                mostVisitedDrink.DrinkId = null;
            }
            else
            {
                var data = activities
                    .GroupBy(x => x.DrinkId)
                    .Select(x => new
                    {
                        drinkId = x.Key,
                        visitedCount = x.Count(),
                        drinkName = x.Select(x => x.DrinkName).First()
                    })
                    .OrderByDescending(x => x.visitedCount)
                    .First();
                mostVisitedDrink.Count = data.visitedCount;
                mostVisitedDrink.Name = data.drinkName;
                mostVisitedDrink.DrinkId = data.drinkId;
            }
            return mostVisitedDrink;
        }

        public async Task<DrinkData> GetLastReviewedDrink(string username)
        {
            var activities =
                await _activitiesRepository.Get(x => x.Username == username && x.Action == PerformedAction.AddedReview);
            var drinkData = new DrinkData();
            if (activities.Count.Equals(0))
            {
                drinkData.DrinkId = null;
                drinkData.DrinkName = "none";
            }
            else
            {
                var lastlyReviewedDrink = activities.OrderByDescending(x => x.Created).First();
                drinkData.DrinkId = lastlyReviewedDrink.DrinkId;
                drinkData.DrinkName = lastlyReviewedDrink.DrinkName;
            }
            return drinkData;
        }

        public async Task<DrinkData> GetLastAddedFavouriteDrink(string username)
        {
            var activities =
                await _activitiesRepository.Get(x => x.Username == username && x.Action == PerformedAction.AddedToFavourite);
            var drinkData = new DrinkData();
            if (activities.Count.Equals(0))
            {
                drinkData.DrinkId = null;
                drinkData.DrinkName = "none";
            }
            else
            {
                var activity = activities.OrderByDescending(x => x.Created).First();
                drinkData.DrinkId = activity.DrinkId;
                drinkData.DrinkName = activity.DrinkName;
            }
            return drinkData;
        }
    }
}
