using Domain.Enums;
using ReportingModule.API.Data.Repositories;
using ReportingModule.API.ReportDataModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingModule.API.Services
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
            var mostFavouriteData = new TheMostData();
            if (favouritesActivities.Count == 0)
            {
                mostFavouriteData.Count = 0;
                mostFavouriteData.Name = "none";
            }
            else
            {
                var data = favouritesActivities
                    .GroupBy(x => x.DrinkId)
                    .Select(x => new
                    {
                        drinkId = x.Key,
                        drinkName = x.Select(x => x.DrinkName).First(),
                        favouritesCount = x.Count()
                    })
                    .OrderByDescending(x => x.favouritesCount)
                    .First();
                mostFavouriteData.Name = data.drinkName;
                mostFavouriteData.Count = data.favouritesCount;
            }
            return mostFavouriteData;
        }

        public async Task<ScoreData> GetHighestScoreDrinkData(DateTime start, DateTime end)
        {
            var scoreActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview && x.Score != null);
            var highestScoreDrinkData = new ScoreData();
            if (scoreActivities.Count == 0)
            {
                highestScoreDrinkData.DrinkName = "none";
                highestScoreDrinkData.AverageScore = 0;
            }
            else
            {
                var data = scoreActivities
                    .GroupBy(x => x.DrinkId)
                    .Select(x => new
                    {
                        drinkId = x.Key,
                        averageScore = x.Select(y => y.Score).Average(),
                        drinkName = x.Select(x => x.DrinkName).First()
                    })
                    .OrderByDescending(x => x.averageScore)
                    .First();
                highestScoreDrinkData.DrinkName = data.drinkName;
                highestScoreDrinkData.AverageScore = data.averageScore;
            }
            return highestScoreDrinkData;
        }

        public async Task<ScoreData> GetLowestScoreDrinkData(DateTime start, DateTime end)
        {
            var scoreActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview && x.Score != null);
            var lowestScoreDrinkData = new ScoreData();
            if (scoreActivities.Count == 0)
            {
                lowestScoreDrinkData.DrinkName = "none";
                lowestScoreDrinkData.AverageScore = 0;
            }
            else
            {
                var data = scoreActivities
                .GroupBy(x => x.DrinkId)
                .Select(x => new
                {
                    drinkId = x.Key,
                    averageScore = x.Select(y => y.Score).Average(),
                    drinkName = x.Select(x => x.DrinkName).First()
                })
                .OrderBy(x => x.averageScore)
                .First();

                lowestScoreDrinkData.DrinkName = data.drinkName;
                lowestScoreDrinkData.AverageScore = data.averageScore;
            }
            return lowestScoreDrinkData;
        }

        public async Task<TheMostData> GetMostVisitedDrinkData(DateTime start, DateTime end)
        {
            var visitedActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.VisitedDrink);
            var mostVisitedDrinkData = new TheMostData();
            if (visitedActivities.Count == 0)
            {
                mostVisitedDrinkData.Name = "none";
                mostVisitedDrinkData.Count = 0;
            }
            else
            {
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
                mostVisitedDrinkData.Name = data.drinkName;
                mostVisitedDrinkData.Count = data.visitedCount;
            }
            return mostVisitedDrinkData;
        }

        public async Task<TheMostData> GetMostActiveUser(DateTime start, DateTime end)
        {
            var allActivities = await _activitiesRepository.Get(x => x.Created > start && x.Created < end);
            var mostActiveUser = new TheMostData();
            if (allActivities.Count == 0)
            {
                mostActiveUser.Name = "none";
                mostActiveUser.Count = 0;
            }
            else
            {
                var data = allActivities
                    .GroupBy(x => x.Username, activity => activity.Action)
                    .Select(x => new { username = x.Key, activitiesCount = x.Count() })
                    .OrderByDescending(x => x.activitiesCount)
                    .First();
                mostActiveUser.Name = data.username;
                mostActiveUser.Count = data.activitiesCount;
            }
            return mostActiveUser;
        }

        public async Task<TheMostData> GetMostReviewedDrinkData(DateTime start, DateTime end)
        {
            var reviewActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview);
            var mostReviewedDrinkData = new TheMostData();
            if (reviewActivities.Count == 0)
            {
                mostReviewedDrinkData.Name = "none";
                mostReviewedDrinkData.Count = 0;
            }
            else
            {
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
                mostReviewedDrinkData.Name = data.drinkName;
                mostReviewedDrinkData.Count = data.reviewsCount;
            }
            return mostReviewedDrinkData;
        }

        public async Task<int> GetExternalLoginData(DateTime start, DateTime end)
        {
            var externalLoginActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.ExternalLogin);
            return externalLoginActivities.Count();
        }

        public async Task<int> GetNewRegisters(DateTime start, DateTime end)
        {
            var registerActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.NewUserRegistered);
            return registerActivities.Count();
        }
    }
}
