using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IGetReportDataService
    { 
        Task<string> GetSuccessfulLoginsData(DateTime start, DateTime end);
        Task<string> GetMostFavorableDrinkData(DateTime start, DateTime end);
        Task<string> GetHighestScoreDrinkData(DateTime start, DateTime end);
        Task<string> GetLowestScoreDrinkData(DateTime start, DateTime end);
        Task<string> GetMostVisitedDrinkData(DateTime start, DateTime end);
        Task<string> GetMostActiveUser(DateTime start, DateTime end);
        Task<string> GetMostReviewedDrinkData(DateTime start, DateTime end);
        Task<int> GetExternalLoginData(DateTime start, DateTime end);
    }
}
