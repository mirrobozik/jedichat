using System.Threading.Tasks;
using JediChat.Server.Models;

namespace JediChat.Server.Hubs
{
    public interface IChatClient
    {
        Task Message(MessageOutputEvent message);      

        Task Presence(PresenceEvent evt);
    }
}