using System.Collections.Generic;
using System.Threading.Tasks;

namespace JediChat.Server.Services
{
    public class InMemoryUserStore : IUserStore
    {
        public Task AddConnectionAsync(string userId, string connectionId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveConnectionAsync(string connectionId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUserToGroupsAsync(string userId, IEnumerable<string> groups)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUserFromGroupsAsync(string userId, IEnumerable<string> groups)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdForConnectionAsync(string connectionId)
        {
            throw new System.NotImplementedException();
        }
    }
}