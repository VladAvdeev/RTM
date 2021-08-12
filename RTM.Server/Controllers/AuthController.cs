using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTM.Common.Models;
using RTM.Server.Helpers;
using RTM.Server.Repositories;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace RTM.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        [HttpGet("dev-auth")]
        public string DevAuth()
        {
            var user = DataCache.UserRepository.GetAdmin();
            user.Grants.Add("admin");
            return JWTHelper.CreateToken(user);
        }

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
            //user.Token = JWTHelper.CreateToken(user);
            var identity = GetIdentity(user);
            return user;
        }

        [HttpPost("add-user/{name}")]
        public void AddUser(string name)
        {
            DataCache.UserRepository.AddUser(name);
        }
        [HttpPut("rename-user/{id}/{name}")]
        public void RenameUser(int id, string name)
        {
            DataCache.UserRepository.UserRename(id, name);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            user.Token = JWTHelper.CreateToken(user);
           
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Name)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
        }

        [HttpPost("/token")]
        public object Token(string name)
        {
            var user = DataCache.UserRepository.GetUserByName(name);
            if (user is null)
            {
                throw new Exception("Пользователь не существует");
            }
            //user.Token = JWTHelper.CreateToken(user);
            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return response;
        }
        [Authorize]
        [HttpGet("IdentityName")]
        public string GetName()
        {         
            return $"{User?.Identity?.Name}";
        }
    }
}
