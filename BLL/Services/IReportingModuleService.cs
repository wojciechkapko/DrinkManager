using BLL.Enums;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReportingModuleService
    {
        Task CreateUserActivity(PerformedAction action, string? username, string? drinkId, string? searchedPhrase, int? score);
    }
}
