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
        public async Task EnterGroup(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }
        public async Task SendMessage(int chatId, User user, string message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", chatId, user, message);
            DataCache.ChatRepository.AddMessage(chatId, user.Id, message);
        }
    }
}
