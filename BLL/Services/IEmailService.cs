using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IEmailService
    {
        Task SendAdminEmail(Email email);
    }
}