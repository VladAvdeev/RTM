using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using RTM.Common.Models;

namespace RTM.Server.Repositories
{
    public class ChatRepository
    {
        public List<ChatGroup> Chats { get; private set; }
        private int sequenceChat = 0;
        private int sequenceMessage = 0;
        public void AddMessage(int chatId, int userId, string content)
        {
            Message message = new Message();
            message.Id = ++sequenceMessage;
            message.Date = DateTime.Now;
            message.User = DataCache.UserRepository.Users.Where(x => x.Id == userId).FirstOrDefault();
            message.Content = content;
            Chats.Where(x => x.Id == chatId).FirstOrDefault().Messages.Add(message);
        }
        public void CreateChat(ChatGroup chat)
        {
            chat.Id = ++sequenceChat;
            Chats.Add(chat);
        }
        public List<ChatGroup> GetChatGroups(User user)
        {
            var chats = Chats.Where(x => x.Users.Any(x => x.Id == user.Id)).OrderBy(x => x.LastMessage.Date).ToList();
            chats.ForEach(x =>
            {
                if (x.Users.Count == 2)
                    x.Name = x.Users.Where(y => y.Id != user.Id).FirstOrDefault().Name;
                else if (x.Users.Count > 2)
                    x.Name = "Группа";
                x.Messages = x.Messages.OrderBy(y => y.Date).ToList();
            }
            );
            return chats;
        }
        public ChatGroup GetChatGroup(int chatGroupId, User user)
        {
            var chat = Chats.Where(x => x.Id == chatGroupId).OrderBy(x => x.LastMessage.Date).FirstOrDefault();

            if (chat.Users.Count == 2)
                chat.Name = chat.Users.Where(y => y.Id != user.Id).FirstOrDefault().Name;
            else if (chat.Users.Count > 2)
                chat.Name = "Группа";

            chat.Messages = chat.Messages.OrderBy(y => y.Date).ToList();
            return chat;
        }
        public void Init()
        {
            Chats = new List<ChatGroup>()
            {
                new ChatGroup()
                {
                    Id = ++sequenceChat,
                    Users = new List<User>(){DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault() },
                    Messages = new List<Message>()
                    { 
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "qwe", Date = new DateTime(2021, 08, 01, 12, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "rty", Date = new DateTime(2021, 08, 01, 13, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "uio", Date = new DateTime(2021, 08, 01, 12, 42, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "p[]", Date = new DateTime(2021, 08, 01, 11, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "asd", Date = new DateTime(2021, 08, 01, 11, 23, 10) },
                    }                  
                },
                new ChatGroup()
                {
                    Id = ++sequenceChat,
                    Users = new List<User>(){DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault() },
                    Messages = new List<Message>()
                    {
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "wefwefsf!", Date = new DateTime(2021, 08, 02, 12, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "qweasd!", Date = new DateTime(2021, 08, 02, 12, 33, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "tuyjtyjgh?", Date = new DateTime(2021, 08, 02, 12, 34, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "qweqwedasd", Date = new DateTime(2021, 08, 02, 13, 35, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "aweasd", Date = new DateTime(2021, 08, 02, 12, 35, 12) },
                    }
                },
                new ChatGroup()
                {
                    Id = ++sequenceChat,
                    Users = new List<User>(){DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault() },
                    Messages = new List<Message>()
                    {
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "wefwefsf!", Date = new DateTime(2021, 08, 01, 12, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "qweasd!", Date = new DateTime(2021, 08, 01, 12, 33, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "asd", Date = new DateTime(2021, 08, 01, 12, 38, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "tuyjtyjgh?", Date = new DateTime(2021, 08, 01, 12, 34, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "qweqwedasd", Date = new DateTime(2021, 08, 01, 13, 35, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "asd", Date = new DateTime(2021, 08, 01, 13, 40, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "aweasd", Date = new DateTime(2021, 08, 01, 14, 35, 12) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "asd", Date = new DateTime(2021, 08, 01, 14, 50, 10) },
                    }
                }
            };
        }

    }
}
