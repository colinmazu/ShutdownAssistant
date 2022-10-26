using Microsoft.AspNetCore.Mvc;
using ShutdownAssistant.Models;
using ShutdownAssistant.Services;

namespace ShutdownAssistant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SystemController : Controller
    {
        private readonly ILogger<SystemController> _logger;
        private readonly ShutdownAssistantConfig _config;

        public SystemController(ILogger<SystemController> logger, ShutdownAssistantConfig config)
        {
            _logger = logger;
            _config = config;
        }
        [HttpPost(Name = "Hibernate")]
        public IActionResult Hibernate(Request request)
        {
            if (request.apiKey == _config.apiKey)
            {
                Task<bool> task = Suspend.Hibernate(_config.delay);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost(Name = "Sleep")]
        public IActionResult Sleep(Request request)
        {
            if (request.apiKey == _config.apiKey)
            {
                Task<bool> task = Suspend.Sleep(_config.delay);
                return Ok();
            }
            return BadRequest();
        }
    }
}
