using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using TeslaPanelBus;

namespace TeslaPanelBus
{
    class Openable : IOpenable
    {
        protected RestClient restClient;
        private string apiBaseUrl;
        Openable(RestClient restClient)
        {

        }
        public Boolean IsOpen()
        {
            return true;
        }

        public void Open()
        {

        }

        public void Close()
        {

        }
    }
}
