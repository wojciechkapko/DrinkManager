using BLL.Enums;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IReportingApiService
    {
        Task UserDidSomething(PerformedAction action);
    }
}
