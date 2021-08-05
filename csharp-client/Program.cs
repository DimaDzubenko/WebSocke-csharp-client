using Newtonsoft.Json;
using System;
using WebSocketSharp;

/*
 1) The console application for collecting information 
    from computer and sending it to webService. 
    Connection must be set by webSocket. 
    Information for collecting: Computer name, time zone, OS Name, .net version.
 */

namespace csharp_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var pc = new PC
            {
                PCName = Environment.MachineName,
                PCTimeZone = TimeZone.CurrentTimeZone.StandardName,
                PCOSName = Environment.OSVersion.ToString(),
                PCNetVersion = Environment.Version.ToString()
            };

            // link to webService where sending information
            var link = "ws://127.0.0.1:7890/Echo";

            using (var ws = new WebSocket(link))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("PC information: " + e.Data);

                ws.Connect();

                string pcJson = JsonConvert.SerializeObject(pc);

                ws.Send(pcJson);
                Console.ReadKey(true);
            }            
        }
    }

    public class PC
    {
        public string PCName { get; set; }
        public string PCTimeZone { get; set; }
        public string PCOSName { get; set; }
        public string PCNetVersion { get; set; }
    }
}
