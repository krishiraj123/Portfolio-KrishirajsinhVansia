using Microsoft.Extensions.Options;
using Portfolio.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Portfolio.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(ContactFormModel model)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = model.Subject,
                Body = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}",
                IsBodyHtml = false
            };
            mailMessage.To.Add(_emailSettings.SenderEmail);

            using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}