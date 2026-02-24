using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreBackgroundDemo.Services
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly ILogger<NotificationBackgroundService> _logger;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;

        public NotificationBackgroundService(
            ILogger<NotificationBackgroundService> logger,
            EmailService emailService,
            SmsService smsService)
        {
            _logger = logger;
            _emailService = emailService;
            _smsService = smsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Executing scheduled task at {time}", DateTime.Now);

                    // Mock email
                    await _emailService.SendEmailAsync(
                        "user@example.com",
                        "Scheduled Notification",
                        "Hello from background job!"
                    );

                    // Mock SMS
                    await _smsService.SendSmsAsync(
                        "+1234567890",
                        "Hello from background job!"
                    );

                    _logger.LogInformation("Task execution completed at {time}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during background task execution.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // runs every 1 minute
            }
        }
    }
}