using System.Collections.Generic;
using System.Threading.Tasks;
using JediChat.Server.Models;

namespace JediChat.Server.Hubs
{
    public interface IChatClient
    {
        Task Message(ChatMessage message);

        Task Joined(string uuid);

        Task Leaved(string uuid);

        Task Members(IEnumerable<string> users);
    }
}