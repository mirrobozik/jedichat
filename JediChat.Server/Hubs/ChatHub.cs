using System;
using System.Linq;
using System.Threading.Tasks;
using JediChat.Server.Models;
using JediChat.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace JediChat.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        #region Constructor

        private IMessageStore _messageStore;
        private readonly IUserStore _userStore;
        
        public ChatHub(
            IMessageStore messageStore, 
            IUserStore userStore)
        {
            _messageStore = messageStore;
            _userStore = userStore;
        }

        #endregion

        #region Connect/Disconnect Events

        public override async Task OnConnectedAsync()
        {
            var context = Context.GetHttpContext();

            var uuid = context.Request.Query["uuid"];
            if (string.IsNullOrEmpty(uuid))
            {
                Context.Abort(); // abort connection
                return;
            }

            var connectionId = Context.ConnectionId;

            await _userStore.AddConnectionAsync(uuid, connectionId);

            //await _presenceService.AddConnectionAsync(uuid, connectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _userStore.RemoveConnectionAsync(Context.ConnectionId);
        }

        #endregion        

        public async Task Send(MessageInputEvent messageInput)
        {
            var sender = await _userStore.GetUserIdForConnectionAsync(Context.ConnectionId);

            var output = new MessageOutputEvent
            {                
                Message = messageInput.Message,
                Sender = sender
            };

            await Clients
                .Group(messageInput.Group)
                .Message(output);
        }

        public async Task Join(GroupChangeEvent evt)
        {
            //todo add all user connections to group(s)
            var userId = await _userStore.GetUserIdForConnectionAsync(Context.ConnectionId);
            var connections = await _userStore.GetUserConnectionsAsync(userId);

            var tasks = evt.Groups
                .Select(group => Groups.AddToGroupAsync(Context.ConnectionId, group));

            await Task.WhenAll(tasks);
            
            await _userStore.AddUserToGroupsAsync(userId, evt.Groups);

            // send presence event
            var presenceEvents = evt.Groups.Select(g => new PresenceEvent
            {
                Action = PresenceAction.Join,
                Group = g,
                Uuid = userId
            });
            
            var presenceTasks = presenceEvents.Select(e => Clients.GroupExcept(e.Group, connections).Presence(e));

            await Task.WhenAll(presenceTasks);
        }

        public async Task Leave(GroupChangeEvent evt)
        {
            var tasks = evt.Groups
                .Select(group => Groups.RemoveFromGroupAsync(Context.ConnectionId, group));

            await Task.WhenAll(tasks);

            var userId = await _userStore.GetUserIdForConnectionAsync(Context.ConnectionId);
            await _userStore.RemoveUserFromGroupsAsync(userId, evt.Groups);

            //todo send presence event
        }

    }
}