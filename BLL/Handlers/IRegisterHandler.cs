using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using System.Threading.Tasks;

namespace BLL.Handlers
{
    public interface IRegisterHandler
    {
        Task<LoginResponse> Handle(RegisterRequest request);
    }
}