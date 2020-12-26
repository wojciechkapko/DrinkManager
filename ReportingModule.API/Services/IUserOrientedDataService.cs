﻿using ReportingModule.API.ReportDataModels;
using System.Threading.Tasks;

namespace ReportingModule.API.Services
{
    public interface IUserOrientedDataService
    {
        Task<string> GetUserCreationDate(string username);
        Task<int> GetUserLoginsCount(string username);
        Task<LastSeenData> GetTimeSinceLastUserActivity(string username);
        Task<TheMostData> GetMostVisitedDrinkData(string username);
        Task<DrinkData> GetLastReviewedDrink(string username);
        Task<DrinkData> GetLastAddedFavouriteDrink(string username);
    }
}
