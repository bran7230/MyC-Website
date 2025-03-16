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

        public GlobalVariables()
        {
            Name = "GlobalVariable";
            Value = 1 + 1;
            ip = GetIp();
            

            try
            {
                // Create an absolute file path in the current directory.
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.json");

                if (File.Exists(filePath))
                {
                    var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(File.ReadAllText(filePath));
                    count = jsonData != null && jsonData.ContainsKey("times_loaded")
                                ? jsonData["times_loaded"].GetInt32()
                                : 0;
                    // Always update the IP instead of using the stored value
                    ip = GetIp();
                }
               

                else
                {
                    count = 0;
                    jsonstring = JsonSerializer.Serialize(new { times_loaded = count, ip_address = ip, last_loaded = last_time });
                    File.WriteAllText(filePath, jsonstring);
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to data.json: {ex.Message}");
            }
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
