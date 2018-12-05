using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace CoffeeClientIoT
{
    class Program
    {
        private const string Machine01ConnectionString =
            "DEVICE_CONNECTIONSTRING";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Initializing Coffee Machine 01...");
            var deviceclient = await GetDeviceClient();

            Console.WriteLine("Coffee Machine 01 Connected!");

            for (int i = 0; i < 5; i++)
            {

                DataPayload payload = new DataPayload("black");
                await SendMessageToHub(payload, deviceclient);
                Thread.Sleep(2000);
            }

            Console.WriteLine("Press any key to exit....");

            Console.ReadKey();
        }

        private static async Task<DeviceClient> GetDeviceClient()
        {

            var device = DeviceClient.CreateFromConnectionString(Machine01ConnectionString);

            await device.OpenAsync();

            return device;
        }

        private static async Task SendMessageToHub(DataPayload message, DeviceClient device)
        {
            await device.SendEventAsync(new Message(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject((message)))));
            
            Console.WriteLine($"CoffeeType: {message.CoffeeTypeDispensed}");
        }
    }
}
