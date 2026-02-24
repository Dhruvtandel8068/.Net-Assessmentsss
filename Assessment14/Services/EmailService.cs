using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCoreBackgroundDemo.Services
{
    public class EmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Mock sending email by logging
            _logger.LogInformation("Email sent to {toEmail}: {subject} - {body}", toEmail, subject, body);
            return Task.CompletedTask;
        }
    }
}