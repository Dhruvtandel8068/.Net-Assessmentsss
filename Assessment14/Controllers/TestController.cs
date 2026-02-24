using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBackgroundDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Background service is running!");
        }
    }
} 