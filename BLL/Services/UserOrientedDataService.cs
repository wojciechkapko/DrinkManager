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
        public async Task<DateTime> GetUserCreationDate(string username)
        {
            var activity = await _activitiesRepository.Get(x =>
                x.Username == username && x.Action == PerformedAction.NewUserRegistered);
            return activity.First().Created;
        }

        public async Task<int> GetUserLoginsCount(string username)
        {
            var activities = await _activitiesRepository.Get(x =>
                x.Username == username && x.Action == PerformedAction.ExternalLogin ||
                x.Action == PerformedAction.SuccessfulLogin);
            return activities.Count;
        }
        public async Task<TimeSpan> GetTimeSinceLastUserActivity(string username)
        {
            var activities = await _activitiesRepository.Get(x => x.Username == username);
            return DateTime.Now - activities.OrderByDescending(x => x.Created).First().Created;
        }
        public async Task<TheMostData> GetMostVisitedDrinkData(string username)
        {
            var activities =
                await _activitiesRepository.Get(x => x.Username == username && x.Action == PerformedAction.VisitedDrink);
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
            var mostVisitedDrink = new TheMostData()
            {
                Count = data.visitedCount,
                Name = data.drinkName,
                DrinkId = data.drinkId
            };
            return mostVisitedDrink;
        }

        public async Task<DrinkData?> GetLastReviewedDrink(string username)
        {
            var activities =
                await _activitiesRepository.Get(x => x.Username == username && x.Action == PerformedAction.AddedReview);
            var activity = activities.OrderByDescending(x => x.Created).First();
            var drinkData = new DrinkData()
            {
                DrinkId = activity.DrinkId,
                DrinkName = activity.DrinkName
            };
            return drinkData;
        }

        public async Task<DrinkData?> GetLastAddedFavouriteDrink(string username)
        {
            var activities =
                await _activitiesRepository.Get(x => x.Username == username && x.Action == PerformedAction.AddedToFavourite);
            var activity = activities.OrderByDescending(x => x.Created).First();
            var drinkData = new DrinkData()
            {
                DrinkId = activity.DrinkId,
                DrinkName = activity.DrinkName
            };
            return drinkData;
        }
    }
}
