using BLL.Data.Repositories;
using BLL.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public ReportingService(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public async Task<string> GetSuccessfulLoginsData(DateTime start, DateTime end)
        {
            var logins = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.SuccessfulLogin);
            var externalLogins = await GetExternalLoginData(start, end);
            return $"Users logged in {logins.Count + externalLogins} times, where there was {externalLogins} external logins";
        }

        public async Task<string> GetMostFavorableDrinkData(DateTime start, DateTime end)
        {
            var favouritesActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedToFavourite);
            var data = favouritesActivities
                .GroupBy(x => x.DrinkId, UserActivity => UserActivity.Action)
                .Select(x => new {drinkId = x.Key, favouritesCount = x.Key.Count()})
                .OrderByDescending(x => x.favouritesCount)
                .First();

            return $"{data.drinkId} is the most favourite drink and was added to favourites {data.favouritesCount} times";
        }

        public async Task<string> GetHighestScoreDrinkData(DateTime start, DateTime end)
        {
            var scoreActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview && x.Score != null);
            var data = scoreActivities
                .GroupBy(x => x.DrinkId, userActivity => userActivity.Score)
                .Select(x => new { drinkId = x.Key, averageScore = x.Average() })
                .OrderByDescending(x => x.averageScore)
                .First();

            return $"{data.drinkId} has the highest score: {data.averageScore}";
        }

        public async Task<string> GetLowestScoreDrinkData(DateTime start, DateTime end)
        {
            var scoreActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview && x.Score != null);
            var data = scoreActivities
                .GroupBy(x => x.DrinkId, userActivity => userActivity.Score)
                .Select(x => new { drinkId = x.Key, averageScore = x.Average() })
                .OrderBy(x => x.averageScore)
                .First();

            return $"{data.drinkId} has the lowest score: {data.averageScore}";
        }

        public async Task<string> GetMostVisitedDrinkData(DateTime start, DateTime end)
        {
            var visitedActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.VisitedDrink);
            var data = visitedActivities
                .GroupBy(x => x.DrinkId, userActivity => userActivity.Action)
                .Select(x => new {drinkId = x.Key, visitedCount = x.Count()})
                .OrderByDescending(x => x.visitedCount)
                .First();

            return $"{data.drinkId} is the most visited drinks and was visited {data.visitedCount} times";
        }

        public async Task<string> GetMostActiveUser(DateTime start, DateTime end)
        {
            var allActivities = await _activitiesRepository.Get();
            var data = allActivities
                .GroupBy(x => x.Username, activity => activity.Action)
                .Select(x => new {username = x.Key, activitiesCount = x.Count()})
                .OrderByDescending(x => x.activitiesCount)
                .First();

            return $"{data.username} is the most active user";
        }

        public async Task<string> GetMostReviewedDrinkData(DateTime start, DateTime end)
        {
            var reviewActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.AddedReview);
            var data = reviewActivities
                .GroupBy(x => x.DrinkId, userActivity => userActivity.Action)
                .Select(x => new {drinkId = x.Key, reviewsCount = x.Count()})
                .OrderByDescending(x => x.reviewsCount)
                .First();

            return $"{data.drinkId} is the most reviewed drink and has {data.reviewsCount} reviews";
        }

        public async Task<int> GetExternalLoginData(DateTime start, DateTime end)
        {
            var externalLoginActivities = await _activitiesRepository.Get(x =>
                x.Created > start && x.Created < end && x.Action == PerformedAction.ExternalLogin);
            return externalLoginActivities.Count();
        }
    }
}
