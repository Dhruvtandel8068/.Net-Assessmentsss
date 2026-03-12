using System.Net;
using System.Net.Mail;
using Assessment14.Options;
using Microsoft.Extensions.Options;

namespace Assessment14.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using var smtp = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
                    EnableSsl = true
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mail.To.Add(to);

                await smtp.SendMailAsync(mail);

                _logger.LogInformation("✅ Email sent successfully to {To}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Email sending failed to {To}", to);
                throw;
            }
        }
    }
}