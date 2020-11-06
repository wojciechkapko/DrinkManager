namespace BLL.Services
{
    public interface IEmailService
    {
        void SendAdminEmail(Email email);
    }
}