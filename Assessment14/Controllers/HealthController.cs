using Assessment14.Models;
using Assessment14.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assessment14.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly NotificationQueue _queue;

    public HealthController(NotificationQueue queue)
    {
        _queue = queue;
    }

    [HttpGet]
    public IActionResult Get() => Ok("Assessment14 API is running ✅");

    [HttpPost("notify")]
    public IActionResult Notify([FromBody] NotificationMessage msg)
    {
        _queue.Enqueue(msg);
        return Ok(new { message = "Notification added to queue ✅" });
    }
}