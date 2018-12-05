using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace CoffeeMessageProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hubName = "HUB_NAME";
            var iotHubConnectionString = "HUB_ENDPOINT";
            var storageConnectionString = "STORAGE_CONNECTIONSTRING";
            var storageContainerName = "message-processor-host";
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;

            var processor = new EventProcessorHost(
                hubName,
                consumerGroupName,
                iotHubConnectionString,
                storageConnectionString,
                storageContainerName);

            await processor.RegisterEventProcessorAsync<CoffeeDispensedEventProcessor>();

            Console.WriteLine("Event processor started, press enter to exit...");

            Console.ReadLine();

            await processor.UnregisterEventProcessorAsync();
        }
    }
}
