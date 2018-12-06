using System;
using System.Threading.Tasks;
using CoffeeCommon;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace CoffeeMessageProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var storageContainerName = "message-processor-host";
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;

            var processor = new EventProcessorHost(
                MyAzure.hubName,
                consumerGroupName,
                MyAzure.iotHubConnectionString,
                MyAzure.storageConnectionString,
                storageContainerName);

            await processor.RegisterEventProcessorAsync<CoffeeDispensedEventProcessor>();

            Console.WriteLine("Event processor started, press enter to exit...");

            Console.ReadLine();

            await processor.UnregisterEventProcessorAsync();
        }
    }
}
