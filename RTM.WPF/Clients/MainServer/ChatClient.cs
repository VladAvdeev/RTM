using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using RTM.Common.Models;
using RTM.WPF.Models;

namespace RTM.WPF.Clients.MainServer
{
    class ChatClient : BaseRestClient
    {
        public ObservableCollection<ChatGroupClient> GetChats(int userId)
        {
            var request = new RestRequest($"api/chat/get-chats/{userId}", Method.GET);
            var response = new RestClient(adress).Execute<List<ChatGroup>>(request);
            var data = response.Data;
            ObservableCollection<ChatGroupClient> obs = new ObservableCollection<ChatGroupClient>();
            foreach (var item in data)
            {
                ChatGroupClient cl = new ChatGroupClient
                {
                    Id = item.Id,
                    Name = item.Name,
                    Users = item.Users.Select(x => new Models.UserClient() { Id = x.Id, Name = x.Name }).ToList()
                };
                foreach (var mes in item.Messages)
                    cl.AddMessage(new MessageClient() { Id = mes.Id, Content = mes.Content, Date = mes.Date, User = new Models.UserClient() { Id = mes.User.Id, Name = mes.User.Name } });
                obs.Add(cl);
            }
            return obs;
        }

        public void SendMessage(int chatId, int userId, string content)
        {
            var request = new RestRequest($"api/chat/send-message/{chatId}/{userId}/{content}", Method.POST);
            var data = CreateClient().Execute(request);
        }
        public ChatGroup RefreshChatGroup(int chatId, int userId)
        {
            var request = new RestRequest($"api/chat/get-chat/{chatId}/{userId}", Method.GET);
            var data = CreateClient().Execute<ChatGroup>(request);
            return data.Data;
        }

    }
}

