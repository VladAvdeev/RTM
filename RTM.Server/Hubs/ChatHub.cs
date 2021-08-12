using Microsoft.AspNetCore.SignalR;
using RTM.Common.Models;
using RTM.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTM.Server.Hubs
{
    public class ChatHub : Hub
    {
        static List<string> connections = new List<string>();
        static List<UserConnection> userConnections = new List<UserConnection>();
        public async Task EnterGroup(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }
        public async Task SendMessage(int chatId, User user, string message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", chatId, user, message);
            DataCache.ChatRepository.AddMessage(chatId, user.Id, message);
        }

        public async Task SendUser(int userId)
        {
            userConnections.Add(new UserConnection { ConnectionId = Context.ConnectionId, UserId = userId });
            await Clients.All.SendAsync("ReshreshOnline", userConnections.Select(x => x.UserId).Distinct().ToList());
        }

        public async Task Connect()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("SendUser");
        }

        public override Task OnConnectedAsync()
        {
            connections.Add(Context.ConnectionId);
            Connect();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            connections.Remove(Context.ConnectionId);
            var user = userConnections.Where(x => x.ConnectionId == Context.ConnectionId).FirstOrDefault();
            userConnections.Remove(user);
            if(!userConnections.Any(x => x.UserId == user.UserId))
                Clients.All.SendAsync("ReshreshOnline", userConnections.Select(x => x.UserId).Distinct().ToList());
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class UserConnection
    { 
        public string ConnectionId { get; set; }
        public int UserId { get; set; }
    }
}
