using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace TeslaPanelBus
{
    static class Client
    {
        static public RestClient restClient;
        static Client()
        {
            restClient = new RestClient(Configs.Configuration["API_BASE_URL"]);
        }
        
        static public void AddAuthHeader(RestRequest restRequest)
        {
            restRequest.AddHeader("Authorization", String.Format("Bearer {0}", Environment.GetEnvironmentVariable("TESLA_ACCESS_TOKEN")));
        }
        
    }
}
