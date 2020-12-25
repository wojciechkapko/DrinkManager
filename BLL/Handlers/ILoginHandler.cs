using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using System.Threading.Tasks;

namespace BLL.Handlers
{
    public interface ILoginHandler
    {
        Task<LoginResponse> Handle(LoginRequest request);
    }
}