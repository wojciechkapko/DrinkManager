using BLL.Enums;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IApiService
    {
        Task CreateUserActivity(PerformedAction action, string? username, string? drinkId, string? searchedPhrase, int? score);
    }
}
