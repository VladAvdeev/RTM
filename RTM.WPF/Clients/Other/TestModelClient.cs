using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RestSharp;
using RTM.WPF.Clients.Other.Models;

namespace RTM.WPF.Clients.Other
{
    class TestModelClient
    {
        protected string adress = "https://localhost:5001";
        public List<TestModel> GetTestModels()
        {
            var request = new RestRequest("Api/GetTestModels", Method.GET);
            var response = new RestClient(adress).Execute(request);
            var data = JsonSerializer.Deserialize<List<TestModel>>(response.Content);
            return data;
        }

        public void GetText()
        {
            var request = new RestRequest("https://www.cbr-xml-daily.ru/daily_json.js", Method.GET);
            var response = new RestClient(adress).Execute(request);
        }
    }
}
