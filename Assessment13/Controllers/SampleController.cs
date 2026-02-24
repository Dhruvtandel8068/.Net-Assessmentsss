using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreLoggingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
        private readonly IMemoryCache _cache;

        public SampleController(ILogger<SampleController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            _logger.LogInformation("GetInfo endpoint called");
            return Ok(new { Message = "Hello from Logging Demo" });
        }

        [HttpGet("cached-data")]
        public IActionResult GetCachedData()
        {
            string cacheKey = "currentTime";
            if (!_cache.TryGetValue(cacheKey, out string? currentTime))
            {
                currentTime = DateTime.Now.ToString();
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };
                _cache.Set(cacheKey, currentTime, cacheOptions);
                _logger.LogInformation("Cache set for key: {cacheKey}", cacheKey);
            }
            else
            {
                _logger.LogInformation("Cache hit for key: {cacheKey}", cacheKey);
            }

            return Ok(new { CachedTime = currentTime });
        }

        [HttpGet("throw-error")]
        public IActionResult ThrowError()
        {
            throw new Exception("This is a test exception");
        }
    }
}
