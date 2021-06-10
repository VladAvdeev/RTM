using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RTM.Common.Models;

namespace RTM.WPF.Clients.MainServer
{
    class ChatClient
    {
        protected string adress = "https://localhost:5001";
        public List<ChatGroup> GetChats(int userId)
        {
            var request = new RestRequest($"api/chat/get-chats/{userId}", Method.GET);
            var data = new RestClient(adress).Execute<List<ChatGroup>>(request);
            return data.Data;
        }
        public void SendMessage(int chatId, int userId, string content)
        {
            var request = new RestRequest($"api/chat/send-message/{chatId}/{userId}/{content}", Method.POST);
            var data = new RestClient(adress).Execute(request);
        }
        public ChatGroup RefreshChatGroup(int chatId, int userId)
        {
            var request = new RestRequest($"api/chat/get-chat/{chatId}/{userId}", Method.GET);
            var data = new RestClient(adress).Execute<ChatGroup>(request);
            return data.Data;
        }
    }
}
