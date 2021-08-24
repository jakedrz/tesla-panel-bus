using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TeslaPanelBus
{
    static class Configs
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static void initConfig()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("config.ini");
            Configuration = configBuilder.Build();
        }
    }
}
