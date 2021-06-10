using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RTM.WPF.Clients.MainServer
{
    class BaseRestClient
    {
        protected string adress = "https://localhost:5001";

        public RestClient CreateClient()
        {
            RestClient client = new RestClient(adress);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            return client;
        }
        
    }
}
