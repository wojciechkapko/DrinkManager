using BLL.ReportDataModels;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReportDataService
    { 
        Task<LoginData> GetSuccessfulLoginsData(DateTime start, DateTime end);
        Task<TheMostData> GetMostFavouriteDrinkData(DateTime start, DateTime end);
        Task<ScoreData> GetHighestScoreDrinkData(DateTime start, DateTime end);
        Task<ScoreData> GetLowestScoreDrinkData(DateTime start, DateTime end);
        Task<TheMostData> GetMostVisitedDrinkData(DateTime start, DateTime end);
        Task<TheMostData> GetMostActiveUser(DateTime start, DateTime end);
        Task<TheMostData> GetMostReviewedDrinkData(DateTime start, DateTime end);
        Task<int> GetExternalLoginData(DateTime start, DateTime end);
        Task<int> GetNewRegisters(DateTime start, DateTime end);
    }
}
