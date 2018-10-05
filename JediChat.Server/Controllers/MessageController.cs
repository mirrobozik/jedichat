using System.Threading.Tasks;
using JediChat.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace JediChat.Server.Controllers
{
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private const int MaxLimit = 100;
        private readonly IMessageStore _messageStore;

        public MessageController(IMessageStore messageStore)
        {
            _messageStore = messageStore;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string users, int limit = MaxLimit)
        {
            if (string.IsNullOrEmpty(users))
            {
                return BadRequest();
            }

            var userIds = users.Split(',');

            var messages = await _messageStore.ListByUserAsync(
                fromUserIds: userIds,
                toUserIds: userIds,
                limit: limit);

            return Ok(messages);
        }
    }
}