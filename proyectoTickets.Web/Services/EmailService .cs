using System.Net;
using System.Net.Mail;

namespace proyectoTickets.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSettings = _config.GetSection("SmtpSettings");
            var message = new MailMessage
            {
                From = new MailAddress(smtpSettings["UserName"]??""),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            using var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"] ?? ""))
            {
                Credentials = new NetworkCredential(smtpSettings["UserName"], smtpSettings["Password"]),
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "")
            };

            await client.SendMailAsync(message);
        }
    }
}
