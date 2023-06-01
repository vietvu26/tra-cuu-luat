using WebApplication1.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace WebApplication1.Services
{
    public interface IEmailService
    {
        Task SendContactEmailAsync(ContactViewModel model);
    }
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;


        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendContactEmailAsync(ContactViewModel model)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailSettings.FromName, _mailSettings.FromEmail));
            message.To.Add(new MailboxAddress("Admin", "ưqfew@gmail.com"));
            message.Subject = "Thông tin liên hệ từ " + model.Name;

            var builder = new BodyBuilder();
            builder.TextBody = "Tên: " + model.Name + "\n"
                     + "Email: " + model.Email + "\n"
                     + "Điện thoại: " + model.Phone + "\n"
                     + "Nội dung: " + model.Message;

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
