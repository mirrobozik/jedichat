using System.Collections.Generic;
using System.Threading.Tasks;
using JediChat.Server.Models;

namespace JediChat.Server.Services
{
    public interface IPresenceService
    {
        Task AddConnectionAsync(string uuid, string connectionId);

        Task RemoveConnectionAsync(string connectionId);
       
        Task<UserInfo> GetUserAsync(string uuid);

        Task<UserInfo> GetUserByConnectionAsync(string connectionId);

        Task<IEnumerable<UserInfo>> AllAsync();
    }
}