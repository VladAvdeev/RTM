using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTM.Common.Models;
using RTM.Server.Repositories;

namespace RTM.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("get-users")]
        public List<User> Users()
        {
            return DataCache.UserRepository.Users;
        }

        [HttpGet("auth-by-name/{name}")]
        public User AuthUser(string name)
        {
            var user = DataCache.UserRepository.GetUserByName(name);
            if (user is null)
            {
                throw new Exception("Пользователь не существует");
            }
            return user;
        }
        [HttpPost("add-user/{name}")]
        public void AddUser(string name)
        {
            DataCache.UserRepository.AddUser(name);
        }
        [HttpPut("rename-user/{id}/name")]
        public void RenameUser(int id, string name)
        {
            DataCache.UserRepository.UserRename(id, name);
        }
    }
}
