using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ShutdownAssistant.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SystemController : Controller
    {
        private readonly ILogger<SystemController> _logger;

        public SystemController(ILogger<SystemController> logger)
        {
            _logger = logger;
        }
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        [HttpGet(Name = "Hibernate")]
        public string Hibernate()
        {
            SetSuspendState(true, true, true);

            return "ok";
        }
        [HttpGet(Name = "Sleep")]
        public string Sleep()
        {
            SetSuspendState(false, true, true);

            return "ok";
        }
    }
}
