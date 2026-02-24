using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment12Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok("Only Admin Can Access");
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public IActionResult UserEndpoint()
        {
            return Ok("Only User Can Access");
        }

        [HttpGet("policy")]
        [Authorize(Policy = "MinimumAge18")]
        public IActionResult PolicyEndpoint()
        {
            return Ok("Policy Based Access Granted");
        }
    }
}
