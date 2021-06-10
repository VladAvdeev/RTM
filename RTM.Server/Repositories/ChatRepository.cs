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

            chat.Messages = chat.Messages.OrderBy(y => y.Date).ToList();
            return chat;
        }
        public void Init()
        {
            Chats = new List<ChatGroup>()
            {
                new ChatGroup() //Чат Димы, Саши
                {
                    Id = ++sequenceChat,
                    Users = new List<User>(){DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault() },
                    Messages = new List<Message>()
                    { 
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "Морф бьёт яйцо", Date = new DateTime(2021, 03, 12, 12, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "На последней тыке разворачивается бьёт Марса", Date = new DateTime(2021, 03, 12, 13, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "И яйцо лопается", Date = new DateTime(2021, 03, 12, 12, 42, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "may be ashan ?", Date = new DateTime(2021, 03, 12, 11, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 2).FirstOrDefault(), Content = "may be go to the mac ?", Date = new DateTime(2021, 03, 12, 11, 23, 10) },
                    }                  
                },
                new ChatGroup() //Чат Димы, Влада
                {
                    Id = ++sequenceChat,
                    Users = new List<User>(){DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault() },
                    Messages = new List<Message>()
                    {
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "Привет!", Date = new DateTime(2021, 03, 13, 12, 32, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "Ку!", Date = new DateTime(2021, 03, 13, 12, 33, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "Как дела?", Date = new DateTime(2021, 03, 13, 12, 34, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 1).FirstOrDefault(), Content = "Нормас", Date = new DateTime(2021, 03, 13, 13, 35, 10) },
                        new Message {Id = ++sequenceMessage, User = DataCache.UserRepository.Users.Where(x => x.Id == 3).FirstOrDefault(), Content = "Збс", Date = new DateTime(2021, 03, 12, 12, 35, 12) },
                    }
                },
            };
        }

    }
}
