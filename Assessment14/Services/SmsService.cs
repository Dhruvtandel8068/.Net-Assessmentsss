using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCoreBackgroundDemo.Services
{
    public class SmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger;
        }

        public Task SendSmsAsync(string phoneNumber, string message)
        {
            // Mock sending SMS by logging
            _logger.LogInformation("SMS sent to {phoneNumber}: {message}", phoneNumber, message);
            return Task.CompletedTask;
        }
    }
}