using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RTM.Common.Models;

namespace RTM.WPF.Clients.MainServer
{
    class AuthClient : BaseRestClient
    {
        public User AuthByName(string name)
        {
            var request = new RestRequest($"api/auth/auth-by-name/{name}", Method.GET);
            var data = CreateClient().Execute<User>(request);
            return data.Data;
        }
    }
}
