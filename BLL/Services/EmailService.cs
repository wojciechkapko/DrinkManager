using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendAdminEmail(Email email)
        {
            var adminEmail = MailboxAddress.Parse(_configuration.GetValue<string>("AdministratorEmail"));

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("drinkmanager.isa@gmail.com", "LY@3Rk4UIdWX9pIV");
            smtp.Send(new MimeMessage
            {
                From = { adminEmail },
                To = { adminEmail },
                Subject = email.Subject,
                Body = new TextPart(TextFormat.Plain) { Text = email.Body }
            });
            smtp.Disconnect(true);
        }
    }
}
