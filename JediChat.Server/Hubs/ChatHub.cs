using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JediChat.Server.Models;
using JediChat.Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace JediChat.Server.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        #region Constructor

        private readonly IPresenceService _presenceService;
        private IMessageStore _messageStore;
        
        public ChatHub(
            IPresenceService presenceService, 
            IMessageStore messageStore)
        {
            _presenceService = presenceService;
            _messageStore = messageStore;
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

            await _presenceService.AddConnectionAsync(uuid, connectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _presenceService.RemoveConnectionAsync(Context.ConnectionId);
        }

        #endregion        

        public async Task Send(ChatMessage message)
        {
            var sender = await _presenceService.GetUserByConnectionAsync(Context.ConnectionId);
            var uuid = message.ToUuid;
            var user = await _presenceService.GetUserAsync(uuid);
            if (user!=null)
            {
                message.FromUuid = sender.Uuid;

                var recipients = new List<string>(user.ConnectionIds)
                {
                    Context.ConnectionId
                };

                await Clients
                    .Clients(recipients)
                    .Message(message);

                _messageStore
                    .AddAsync(message)
                    .ConfigureAwait(false);
            }
        }

    }
}