using System;
using System.Text;
using System.Threading.Tasks;
using CoffeeCommon;
using Microsoft.Azure.Devices;

namespace CoffeeAzureMessageSender
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("***CLOUD MESSAGE SENDER CONSOLE (C2D Sender)***");

            var serviceclient = ServiceClient.CreateFromConnectionString(MyAzure.hubserviceconnectionstring);

            while (true)
            {
                Console.WriteLine("Which device do you wish to send a message to? ");
                Console.Write("> ");
                var deviceID = Console.ReadLine();

                await SendCloudToDeviceMessage(serviceclient, deviceID);
            }
        }

        private static async Task SendCloudToDeviceMessage(
            ServiceClient serviceClient,
            string deviceID)
        {
            Console.WriteLine("What message payload do you want to send? ");
            Console.Write("> ");

            var payload = Console.ReadLine();

            var commandMessage = new Message(Encoding.ASCII.GetBytes(payload));

            await serviceClient.SendAsync(deviceID, commandMessage);
        }
    }
}
