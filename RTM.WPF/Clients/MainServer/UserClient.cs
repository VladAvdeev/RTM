using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTM.WPF.Clients.MainServer
{
    class UserClient : BaseRestClient
    {
        public void RenameUser(int id, string newName)
        {
            var request = new RestRequest($"api/auth/rename-user/{id}/{newName}", Method.PUT);
            var data = CreateClient().Execute(request);
        }
        public void RefreshUsers()
        {

        }
    }
}
