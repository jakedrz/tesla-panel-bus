using System;

using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TeslaPanelBus
{
    partial class Program
    {
        public static int refreshSleep, refreshAwake, currentRefresh;
        public static Timer timer;
        public static IConfigurationRoot Configuration { get; set; }

        private static TeslaVehicle teslaVehicle;
        
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new TextWriterTraceListener("tesla-panel-bus.log"));
            Trace.AutoFlush = true;
            ConsoleLogWriteLineNoTimestamp(splashText);
            Configs.initConfig();
            printConfig();

            currentRefresh = refreshSleep = int.Parse(Configs.Configuration["REFRESH_SLEEP"]) * 1000;
            refreshAwake = int.Parse(Configs.Configuration["REFRESH_AWAKE"]) * 1000;

            teslaVehicle = new TeslaVehicle();

            var autoEvent = new AutoResetEvent(false);
            timer = new Timer(timerCallback, autoEvent, 0, refreshSleep);
            autoEvent.WaitOne(Timeout.Infinite);
        }

        private static void timerCallback(object state)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)state;
            
            if (teslaVehicle.isOnline())
            {
                if (currentRefresh == refreshSleep)
                {
                    currentRefresh = refreshAwake;
                    timer.Change(0, refreshAwake);
                }
                Console.WriteLine("Vehicle online");

            }
            else
            {
                if (currentRefresh == refreshAwake)
                {
                    currentRefresh = refreshSleep;
                    timer.Change(0, refreshSleep);
                }
                Console.WriteLine("Vehicle offline");
            }
        }

        static void printConfig()
        {
            ConsoleLogWriteLine("Environment Variables:");
            string accessToken = Environment.GetEnvironmentVariable("TESLA_ACCESS_TOKEN");
            ConsoleLogWriteLine("\t{0,-19} {1}", "TESLA_ACCESS_TOKEN:", accessToken.Substring(accessToken.Length - 4));
            ConsoleLogWriteLine("\t{0,-19} {1}\n", "TESLA_CAR_ID:", Environment.GetEnvironmentVariable("TESLA_CAR_ID"));

            ConsoleLogWriteLine("INI Config:");
            ConsoleLogWriteLine("\t{0,-19} {1}", "AUTH_BASE_URL:", Configs.Configuration["AUTH_BASE_URL"]);
            ConsoleLogWriteLine("\t{0,-19} {1}", "API_BASE_URL:", Configs.Configuration["API_BASE_URL"]);
            ConsoleLogWriteLine("\t{0,-19} {1}", "REFRESH_AWAKE:", Configs.Configuration["REFRESH_AWAKE"]);
            ConsoleLogWriteLine("\t{0,-19} {1}\n", "REFRESH_SLEEP:", Configs.Configuration["REFRESH_SLEEP"]);
        }
        static void ConsoleLogWriteLine(string value, params string[] args)
        {
            Trace.WriteLine(String.Format("[{0} {1}] {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), String.Format(value, args)));
            Console.WriteLine(value, args);
        }
        static void ConsoleLogWriteLineNoTimestamp(string value, params string[] args)
        {
            Trace.WriteLine(String.Format(value, args));
            Console.WriteLine(value, args);
        }
        //static void ConsoleLogWriteLineTimestamp()
    }
}
