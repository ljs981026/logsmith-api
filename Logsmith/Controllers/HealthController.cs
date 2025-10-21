using Microsoft.AspNetCore.Mvc;

namespace Logsmith.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new {status = "ok", time = DateTime.UtcNow.AddHours(9)});
        }
    }
}
