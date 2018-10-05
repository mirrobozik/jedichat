using System.Collections.Generic;
using System.Threading.Tasks;
using JediChat.Server.Models;

namespace JediChat.Server.Services
{
    public interface IMessageStore
    {
        Task AddAsync(ChatMessage message);

        Task DeleteAsync(string id);

        Task UpdateAsync(ChatMessage message);

        Task<ChatMessage> GetAsync(string id);

        Task<IEnumerable<ChatMessage>> ListByUserAsync(string[] fromUserIds, string[] toUserIds, int limit);
    }
}