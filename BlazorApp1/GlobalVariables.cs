using BlazorApp1.Components.Pages;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace BlazorApp1
{
    public class GlobalVariables
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ip { get; set; }
        public int count { get; set; }
        public string jsonstring { get; set; }
        public string last_time { get; set; }
        public string url { get; set; }
        public string query { get; set; }

        public GlobalVariables()
        {
            Name = "Json Webscrape";
            ip = GetIp();
            url = String.Empty;
            query = String.Empty; 

            
        }

        public static string GetIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var address in host.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
