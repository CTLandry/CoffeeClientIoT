using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoffeeCommon;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;

namespace CoffeeClientIoT
{
    class Program
    {


        static async Task Main(string[] args)
        {
            Console.WriteLine("***IoT SIMULATED DEVICE***");
            Console.WriteLine("Initializing Coffee Machine 01...");
            var deviceclient = await GetDeviceClient();
            var receiveEventstask = ReceiveEvents(deviceclient);

            Console.WriteLine("Coffee Machine 01 Connected!");

            await UpdateTwin(deviceclient);

            Console.WriteLine("Select a coffee type: ");
            Console.WriteLine("b: Black");
            Console.WriteLine("e: Espresso");
            Console.WriteLine("l: Latte");
            Console.WriteLine("q: Quit");

            var quitRequested = false;
            while (!quitRequested)
            {
               
                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                CoffeeDispensedData payload;

                switch (Char.ToLower(input))
                {
                    case 'b':
                        {
                            payload = new CoffeeDispensedData(CoffeeDescription.CoffeeType.Black, 120.010, 80.11);
                            await SendMessageToHub(payload, deviceclient);
                           
                            break;
                        }
                    case 'e':
                        {
                            payload = new CoffeeDispensedData(CoffeeDescription.CoffeeType.Espresso, 120.010, 80.11);
                            await SendMessageToHub(payload, deviceclient);
                          
                            break;
                        }
                    case 'l':
                        {
                            payload = new CoffeeDispensedData(CoffeeDescription.CoffeeType.Latte, 120.010, 80.11);
                            await SendMessageToHub(payload, deviceclient);
                           
                            break;
                        }
                    case 'q':
                        {
                            quitRequested = true;
                            break;
                        }
                }

             

            }





        }

        private static async Task<DeviceClient> GetDeviceClient()
        {

            var device = DeviceClient.CreateFromConnectionString(MyAzure.Machine01ConnectionString);

            await device.OpenAsync();

            return device;
        }

        private static async Task SendMessageToHub(CoffeeDispensedData message, DeviceClient device)
        {
            await device.SendEventAsync(new Message(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject((message)))));

            Console.WriteLine($"Order: {message.CoffeeType} sent!");
        }

        private static async Task UpdateTwin(DeviceClient device)
        {
            var twinProperties = new TwinCollection();
            twinProperties["connectiontype"] = "wi-fi";
            twinProperties["connectionStrength"] = "full";

            await device.UpdateReportedPropertiesAsync(twinProperties);
        }

        private static async Task ReceiveEvents(DeviceClient device)
        {
            while (true)
            {
                var message = await device.ReceiveAsync();

                if (message == null)
                {
                    continue;
                }

                var messagebody = message.GetBytes();

                var payload = Encoding.ASCII.GetString(messagebody);

                Console.WriteLine($"Received message from cloud: '{payload}'");

                await device.CompleteAsync(message);

            }
        }
    }
}
