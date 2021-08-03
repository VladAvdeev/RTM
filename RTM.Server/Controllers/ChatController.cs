using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTM.Common.Models;
using RTM.Server.Helpers;
using RTM.Server.Repositories;

namespace RTM.Server.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        [HttpGet("get-chats/{userId}")]
        public List<ChatGroup> GetChats(int userId)
        {
            var User = HttpContext.Request.CheckToken();
            return DataCache.ChatRepository.GetChatGroups(DataCache.UserRepository.Users.Where(x => x.Id == userId).FirstOrDefault());
        }
        [HttpPost("send-message/{chatId}/{userId}/{content}")]
        public void SendMessage(int chatId, int userId, string content)
        {
            DataCache.ChatRepository.AddMessage(chatId, userId, content);
        }
        [HttpGet("get-chat/{chatId}/{userId}")]
        public ChatGroup GetChat(int chatId, int userId)
        {
            return DataCache.ChatRepository.GetChatGroup(chatId, DataCache.UserRepository.Users.Where(x => x.Id == userId).FirstOrDefault());
        }

    }
}
