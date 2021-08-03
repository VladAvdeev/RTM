using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RTM.Server.Hubs
{
    //[Authorize]
    public class NotificationHub : Hub
    {
        public async Task SendAll(string message)
        {
            await Clients.All.SendAsync("ReceiveAll", message);
        }
        public async Task SendTo(string message, string to)
        {
            await Clients.User(to).SendAsync("Receive", message, Context.ConnectionId);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Register(long userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString(CultureInfo.InvariantCulture));

        }

        public Task ToGroup(dynamic id, string message)
        {
            return Clients.Group(id.ToString()).SendMessage(message);
        }
    }
}
