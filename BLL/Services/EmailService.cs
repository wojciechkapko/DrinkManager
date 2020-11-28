using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;
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

        public async Task SendAdminEmail(Email email)
        {
            Console.WriteLine("Sending email");
            var adminEmail = MailboxAddress.Parse(_configuration.GetValue<string>("AppSettings:AdminUserEmail"));

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("drinkmanager.isa@gmail.com", "LY@3Rk4UIdWX9pIV");
            await smtp.SendAsync(new MimeMessage
            {
                From = { adminEmail },
                To = { adminEmail },
                Subject = email.Subject,
                Body = new TextPart(TextFormat.Plain) { Text = email.Body }
            });
            await smtp.DisconnectAsync(true);
        }
    }
}
