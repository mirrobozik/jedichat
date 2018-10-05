using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JediChat.Server.Models;

namespace JediChat.Server.Services
{
    public class InMemoryMessageStore : IMessageStore
    {
        private readonly IList<ChatMessage> _messages = new List<ChatMessage>();

        public Task AddAsync(ChatMessage message)
        {
            lock (_messages)
            {
                _messages.Add(message);
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(ChatMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChatMessage> GetAsync(string id)
        {
            lock (_messages)
            {
                var message = _messages.SingleOrDefault(m => m.Id == id);
                return Task.FromResult(message);
            }
        }

        public Task<IEnumerable<ChatMessage>> ListByUserAsync(string[] fromUserIds, string[] toUserIds, int limit)
        {
            lock (_messages)
            {
                var filtered = _messages.Where(m =>
                    fromUserIds.Contains(m.FromUuid) ||
                    toUserIds.Contains(m.ToUuid)
                    )
                    .OrderBy(m=>m.SentUTC)
                    .TakeLast(limit);

                return Task.FromResult(filtered);
            }
        }
    }
}