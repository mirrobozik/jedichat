using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JediChat.Server.Hubs;
using JediChat.Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace JediChat.Server.Services
{
    public class PresenceService : IPresenceService
    {
        private readonly ConcurrentDictionary<string, UserInfo> _users = new ConcurrentDictionary<string, UserInfo>();
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;

        public PresenceService(IHubContext<ChatHub, IChatClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task AddConnectionAsync(string uuid, string connectionId)
        {

            var user = _users.GetOrAdd(uuid, _ => new UserInfo
            {
                Uuid = uuid,
                ConnectionIds = new HashSet<string>()
            });

            //lock (user.ConnectionIds)
            //{

            //    user.ConnectionIds.Add(connectionId);

            //    if (user.ConnectionIds.Count == 1)
            //    {
            //        var onlineEvent = new PresenceEvent(PresenceEvent.MemberStateOnline, user.Uuid);
            //        _hubContext.Clients
            //            .AllExcept(user.ConnectionIds.ToArray())
            //            .Presence(onlineEvent);
            //    }

            //    var members = _users.Values
            //                    .Where(u=>u.Uuid!=uuid)
            //                    .Select(v=>v.Uuid)
            //                    .ToArray();
            //    _hubContext.Clients
            //        .Client(connectionId)
            //        .Members(members)
            //        ;
            //}

            return Task.CompletedTask;
        }

        public Task RemoveConnectionAsync(string connectionId)
        {
            UserInfo user = _users.Values.SingleOrDefault(u=>u.ConnectionIds.Any(c=>c.Equals(connectionId)));
            
            if (user != null)
            {
                //lock (user.ConnectionIds)
                //{

                //    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                //    if (!user.ConnectionIds.Any())
                //    {
                //        var offlineEvent = new PresenceEvent(PresenceEvent.MemberStateOffline, user.Uuid);
                        
                //        _users.TryRemove(user.Uuid, out _);

                //        // You might want to only broadcast this info if this 
                //        // is the last connection of the user and the user actual is 
                //        // now disconnected from all connections.                        
                //        _hubContext.Clients
                //            .AllExcept(connectionId)
                //            .Presence(offlineEvent);
                //    }
                //}
            }
            return Task.CompletedTask;
        }

        public Task<UserInfo> GetUserAsync(string uuid)
        {
            var user = _users.Values.SingleOrDefault(u => u.Uuid == uuid);
            return Task.FromResult(user);
        }

        public Task<UserInfo> GetUserByConnectionAsync(string connectionId)
        {
            var user = _users.Values.SingleOrDefault(u => u.ConnectionIds.Any(c=>c.Equals(connectionId)));
            return Task.FromResult(user);
        }

        public Task<IEnumerable<UserInfo>> AllAsync()
        {
            var users = _users.Values.ToList();
            return Task.FromResult<IEnumerable<UserInfo>>(users);
        }
    }
}