using Microsoft.AspNetCore.Mvc;

namespace Assessment16.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TestController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "OK",
                project = _config["App:AppName"],
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                developer = "Dhruv Tandel"
            });
        }

        /// <summary>
        /// Checks if SecretKey is loaded (without exposing secret)
        /// </summary>
        [HttpGet("secret-check")]
        public IActionResult SecretCheck()
        {
            var secret = _config["App:SecretKey"];

            return Ok(new
            {
                hasSecret = !string.IsNullOrWhiteSpace(secret),
                secretLength = string.IsNullOrWhiteSpace(secret) ? 0 : secret.Length
            });
        }

        /// <summary>
        /// Crash endpoint to test global middleware
        /// </summary>
        [HttpGet("crash")]
        public IActionResult Crash()
        {
            throw new Exception("Test exception - middleware working.");
        }
    }
}