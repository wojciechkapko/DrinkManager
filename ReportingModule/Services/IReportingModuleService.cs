using BLL;
using Domain.Enums;
using ReportingModuleApi.Data;
using System;
using System.Threading.Tasks;

namespace ReportingModuleApi.Services
{
    public interface IReportingModuleService
    {
        Task CreateUserActivity(PerformedAction action, string username = null, string drinkId = null, string drinkName = null, string searchedPhrase = null, int? score = null);
        Task<Report> GetReportData(DateTime start, DateTime end);
        Task<UserReport> GetUserReportData(string username);
    }
}
