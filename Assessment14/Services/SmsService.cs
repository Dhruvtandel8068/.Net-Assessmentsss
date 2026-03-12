namespace Assessment14.Services;

public class SmsService
{
    private readonly ILogger<SmsService> _logger;

    public SmsService(ILogger<SmsService> logger)
    {
        _logger = logger;
    }

    public Task SendSmsAsync(string phone, string message)
    {
        // ✅ Simulation (mock)
        _logger.LogInformation("SIMULATED SMS => Phone:{phone}, Message:{message}", phone, message);
        return Task.CompletedTask;
    }
}