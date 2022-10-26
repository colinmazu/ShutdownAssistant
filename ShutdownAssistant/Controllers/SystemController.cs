using Microsoft.AspNetCore.Mvc;
using ShutdownAssistant.Models;
using System.Runtime.InteropServices;

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
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        [HttpPost(Name = "Hibernate")]
        public IActionResult Hibernate(Request request)
        {
            if (request.apiKey == _config.apiKey)
            {
                SetSuspendState(true, true, true);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost(Name = "Sleep")]
        public IActionResult Sleep(Request request)
        {
            if (request.apiKey == _config.apiKey)
            {
                SetSuspendState(false, true, true);
                return Ok();
            }
            return BadRequest();
        }
    }
}
