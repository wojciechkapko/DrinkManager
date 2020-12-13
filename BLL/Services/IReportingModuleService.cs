using BLL.Enums;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReportingModuleService
    {
        Task CreateUserActivity(PerformedAction action, string username = null, string drinkId = null, string drinkName = null, string searchedPhrase = null, int? score = null);
        Task<Report> GetReportData(DateTime start, DateTime end);
    }
}
