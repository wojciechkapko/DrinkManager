using BLL.Data.Repositories;
using BLL.Enums;
using BLL.ReportDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportDataService : IReportDataService
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public ReportDataService(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public async Task<LoginData> GetSuccessfulLoginsData(DateTime start, DateTime end)
        {
            var logins = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.SuccessfulLogin);
            var externalLogins = await GetExternalLoginData(start, end);
            var loginsData = new LoginData()
            {
                ExternalLogins = externalLogins,
                AllLogins = logins.Count + externalLogins
            };
            return loginsData;
        }

        public async Task<TheMostData> GetMostFavouriteDrinkData(DateTime start, DateTime end)
        {
            var favouritesActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedToFavourite);
            var data = favouritesActivities
                .GroupBy(x => x.DrinkId)
                .Select(x => new
                {
                    drinkId = x.Key, 
                    drinkName = x.Select(x => x.DrinkName).First(), 
                    favouritesCount = x.Key.Count()
                })
                .OrderByDescending(x => x.favouritesCount)
                .First();
            var mostFavouriteData = new TheMostData()
            {
                Name = data.drinkName,
                Count = data.favouritesCount
            };
            return mostFavouriteData;
        }

        public async Task<ScoreData> GetHighestScoreDrinkData(DateTime start, DateTime end)
        {
            var scoreActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview && x.Score != null);
            var data = scoreActivities
                .GroupBy(x => x.Id)
                .Select(x => new
                {
                    drinkId = x.Key, 
                    averageScore = x.Select(y => y.Score).Average(), 
                    drinkName = x.Select(x => x.DrinkName).First()
                })
                .OrderByDescending(x => x.averageScore)
                .First();
            var highestScoreDrinkData = new ScoreData()
            {
                AverageScore = data.averageScore,
                DrinkName = data.drinkName
            };
            return highestScoreDrinkData;
        }

        public async Task<ScoreData> GetLowestScoreDrinkData(DateTime start, DateTime end)
        {
            var scoreActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview && x.Score != null);
            var data = scoreActivities
                .GroupBy(x => x.DrinkId)
                .Select(x => new {
                    drinkId = x.Key,
                    averageScore = x.Select(y => y.Score).Average(),
                    drinkName = x.Select(x => x.DrinkName).First()
                })
                .OrderBy(x => x.averageScore)
                .First();
            var lowestScoreDrinkData = new ScoreData()
            {
                AverageScore = data.averageScore,
                DrinkName = data.drinkName
            };
            return lowestScoreDrinkData;
        }

        public async Task<TheMostData> GetMostVisitedDrinkData(DateTime start, DateTime end)
        {
            var visitedActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.VisitedDrink);
            var data = visitedActivities
                .GroupBy(x => x.DrinkId)
                .Select(x => new
                {
                    drinkId = x.Key, 
                    visitedCount = x.Count(), 
                    drinkName = x.Select(x => x.DrinkName).First()
                })
                .OrderByDescending(x => x.visitedCount)
                .First();
            var mostVisitedDrinkData = new TheMostData()
            {
                Name = data.drinkName,
                Count = data.visitedCount
            };
            return mostVisitedDrinkData;
        }

        public async Task<TheMostData> GetMostActiveUser(DateTime start, DateTime end)
        {
            var allActivities = await _activitiesRepository.Get();
            var data = allActivities
                .GroupBy(x => x.Username, activity => activity.Action)
                .Select(x => new {username = x.Key, activitiesCount = x.Count()})
                .OrderByDescending(x => x.activitiesCount)
                .First();
            var mostActiveUser = new TheMostData()
            {
                Name = data.username,
                Count = data.activitiesCount
            };

            return mostActiveUser;
        }

        public async Task<TheMostData> GetMostReviewedDrinkData(DateTime start, DateTime end)
        {
            var reviewActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview);
            var data = reviewActivities
                .GroupBy(x => x.DrinkId)
                .Select(x => new
                {
                    drinkId = x.Key, 
                    reviewsCount = x.Count(),
                    drinkName = x.Select(x => x.DrinkName).First()
                })
                .OrderByDescending(x => x.reviewsCount)
                .First();
            var mostReviewedDrinkData = new TheMostData()
            {
                Name = data.drinkName,
                Count = data.reviewsCount
            };
            return mostReviewedDrinkData;
        }

        public async Task<int> GetExternalLoginData(DateTime start, DateTime end)
        {
            var externalLoginActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.ExternalLogin);
            return externalLoginActivities.Count();
        }
    }
}
