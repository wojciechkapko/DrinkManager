using BLL.ReportDataModels;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserOrientedDataService
    {
        Task<DateTime> GetUserCreationDate(string username);
        Task<int> GetUserLoginsCount(string username);
        Task<TimeSpan> GetTimeSinceLastUserActivity(string username);
        Task<TheMostData> GetMostVisitedDrinkData(string username);
        Task<DrinkData?> GetLastReviewedDrink(string username);
        Task<DrinkData?> GetLastAddedFavouriteDrink(string username);
    }
}
