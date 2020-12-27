using Domain;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(AppUser user);
    }
}
