using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IMyService _myService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IConfiguration configuration,
        IMyService myService,
        ILogger<HomeController> logger)
    {
        _configuration = configuration;
        _myService = myService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        string appName = _configuration["AppSettings:AppName"] ?? "Default App Name";
        string version = _configuration["AppSettings:Version"] ?? "No Version";
        string serviceMessage = _myService.GetMessage();
        _logger.LogInformation("Index page loaded successfully");
        ViewBag.AppName = appName;
        ViewBag.Version = version;
        ViewBag.ServiceMessage = serviceMessage;

        return View();
    }
}
