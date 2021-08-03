using Jose;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTM.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RTM.Server.Helpers
{
    public static class JWTHelper
    {
        private static JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings()
        {
            MissingMemberHandling = JsonConfigurate.JsonSettings.MissingMemberHandling,
            NullValueHandling = JsonConfigurate.JsonSettings.NullValueHandling,
            DefaultValueHandling = JsonConfigurate.JsonSettings.DefaultValueHandling,
            DateFormatString = JsonConfigurate.JsonSettings.DateFormatString,
            ReferenceLoopHandling = JsonConfigurate.JsonSettings.ReferenceLoopHandling,
            ContractResolver = new ExceptionResolver(),
        };
        static JWTHelper()
        {
        }
        public static string CreateToken(User user)
        {
            string[] grants = user.Grants?.ToArray();
            user.Grants = null;
            string sub = JsonConvert.SerializeObject(user, Formatting.None, JsonSettings);
            var baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var payload = new Dictionary<string, object>()
            {
                { "sub", Convert.ToBase64String(Encoding.UTF8.GetBytes(sub)) },
                { "exp", DateTime.UtcNow.AddMinutes(AuthOptions.LIFETIME)},
                { "iat", (int)(DateTime.UtcNow - baseDate).TotalSeconds },
                { "nbf", (int)(DateTime.UtcNow - baseDate).TotalSeconds },
                { "role", grants}
            };

            string token = JWT.Encode(payload, AuthOptions.GetKey(), JwsAlgorithm.HS512);
            return token;
        }

        public static User ReadToken(string token)
        {
            User user = new User();
            string payload = Jose.JWT.Decode(token, AuthOptions.GetKey(), JwsAlgorithm.HS512);
            Dictionary<string, object> dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(payload, JsonSettings);
            //DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((long)dict["exp"]);
            DateTime dt = (DateTime)dict["exp"];
            if (dt < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Токен устарел");
            user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(Convert.FromBase64String((string)dict["sub"])));
            //user.Grants = ((JArray)dict["role"]).ToObject<List<string>>();
            return user;
        }

        public static User CheckToken(this HttpRequest request)
        {
            var headers = request.Headers;
            if (headers.ContainsKey("Authorization"))
                return ReadToken(headers["Authorization"].FirstOrDefault().Split(' ')[1]);
            else
                throw new Exception($"Необходим токен. {HttpStatusCode.Unauthorized}");
        }
    }
}
