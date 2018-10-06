using System.Collections.Generic;
using System.Threading.Tasks;

namespace JediChat.Server.Services
{
    public interface IUserStore
    {
        Task AddConnectionAsync(string userId, string connectionId);

        Task RemoveConnectionAsync(string connectionId);

        Task AddUserToGroupsAsync(string userId, IEnumerable<string> groups);

        Task RemoveUserFromGroupsAsync(string userId, IEnumerable<string> groups);

        Task<string> GetUserIdForConnectionAsync(string connectionId);

        Task<IReadOnlyList<string>> GetUserConnectionsAsync(string userId);
    }
}