using System;

using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace TeslaPanelBus
{
    partial class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static TeslaVehicle teslaVehicle;
        
        static void Main(string[] args)
        {
            Console.WriteLine(splashText);
            Configs.initConfig();
            printConfig();
            
            teslaVehicle = new TeslaVehicle();
            
            while(true)
            {
                
                while(teslaVehicle.isOnline())
                {
                    Console.WriteLine("Vehicle online");
                    Task.Delay(int.Parse(Configs.Configuration["REFRESH_AWAKE"])*1000).Wait();
                }
                Console.WriteLine("Vehicle offline");
                Task.Delay(int.Parse(Configs.Configuration["REFRESH_SLEEP"]) * 1000).Wait();
            }

        }
        static void printConfig()
        {
            string accessToken = Environment.GetEnvironmentVariable("TESLA_ACCESS_TOKEN");
            Console.WriteLine("{0,-18} {1}", "AUTH URL:", Configs.Configuration["AUTH_BASE_URL"]);
            Console.WriteLine("{0,-18} {1}", "Access Token:", accessToken.Substring(accessToken.Length - 4));
            Console.WriteLine("{0,-18} {1}", "API URL:", Configs.Configuration["API_BASE_URL"]);
            Console.WriteLine("{0,-18} {1}", "(Vehicle) ID:", Environment.GetEnvironmentVariable("TESLA_CAR_ID"));
            Console.WriteLine("{0,-18} {1}", "REFRESH_AWAKE:", Configs.Configuration["REFRESH_AWAKE"]);
            Console.WriteLine("{0,-18} {1}", "REFRESH_SLEEP:", Configs.Configuration["REFRESH_SLEEP"]);
        }
    }
}
