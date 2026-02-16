using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IMyService _myService;
    

    public HomeController(IConfiguration configuration, IMyService myService)
    {
        _configuration = configuration;
        _myService = myService;
    }



    public IActionResult Index()
    {
        // TASK 2: Read configuration value
        string appName = _configuration["AppSettings:AppName"];

        // TASK 4: Use injected service
        string serviceMessage = _myService.GetMessage();

        ViewBag.AppName = appName;
        ViewBag.ServiceMessage = serviceMessage;

        return View();
    }
}
