using Assessment14.Services;

namespace Assessment14.BackgroundJobs;

public class NotificationWorker : BackgroundService
{
    private readonly ILogger<NotificationWorker> _logger;
    private readonly NotificationQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;

    public NotificationWorker(
        ILogger<NotificationWorker> logger,
        NotificationQueue queue,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _queue = queue;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("✅ NotificationWorker started at {time}", DateTime.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("⏳ Background job running at {time}", DateTime.Now);

                // ✅ Create scope to safely use scoped services inside singleton background worker
                using var scope = _scopeFactory.CreateScope();
                var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                var smsService = scope.ServiceProvider.GetRequiredService<SmsService>();

                while (_queue.TryDequeue(out var msg) && msg != null)
                {
                    await emailService.SendEmailAsync(msg.ToEmail, msg.Subject, msg.Body);
                    await smsService.SendSmsAsync(msg.ToPhone, msg.Body);
                }

                _logger.LogInformation("✅ Background job completed at {time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error inside background worker at {time}", DateTime.Now);
            }

            // ✅ run every 1 minute
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }

        _logger.LogInformation("🛑 NotificationWorker stopped at {time}", DateTime.Now);
    }
}