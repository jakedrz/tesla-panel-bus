using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Microsoft.Extensions.Configuration;

namespace TeslaPanelBus
{
    class TeslaVehicle
    {
        private RestRequest isOnlineRequest;
        public TeslaVehicle()
        {
            initRequests();
        }

        public bool isOnline()
        {
            
            IRestResponse response = Client.restClient.Execute(isOnlineRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                dynamic jsonResponse = RestSharp.SimpleJson.DeserializeObject(response.Content);
                return (jsonResponse.response.state == "online");
            }
            return false;
        }

        private void initRequests()
        {
            isOnlineRequest = new RestRequest(Configs.Configuration["GET_VEHICLE_PATH"].Replace("{id}", Environment.GetEnvironmentVariable("TESLA_CAR_ID")), Method.GET);
            Client.AddAuthHeader(isOnlineRequest);
        }
    }
}
