using System.Threading.Tasks;
using JediChat.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace JediChat.Server.Controllers
{
    [Route("presence")]
    public class PresenceController : ControllerBase
    {
        private IPresenceService _presenceService;

        public PresenceController(IPresenceService presenceService)
        {
            _presenceService = presenceService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            var users = await _presenceService.AllAsync();

            return Ok(users);
        }
    }
}