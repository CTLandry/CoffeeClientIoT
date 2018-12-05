using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace CoffeeMessageProcessor
{
    class CoffeeDispensedEventProcessor : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("CoffeeDispensedEventProcessor Closed, processing partition: " +
                              $"'{context.PartitionId}', reason: '{reason}'");

            return Task.CompletedTask;

        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine("CoffeeDispensedEventProcessor Opened, processing partition: " +
                              $"'{context.PartitionId}'");

            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine("CoffeeDispensedEventProcessor Error, processing partition: " +
                              $"'{context.PartitionId}', error: '{error.Message}'");

            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                var data = Encoding.ASCII.GetString(eventData.Body.Array,
                    eventData.Body.Offset,
                    eventData.Body.Count);

                var deviceID = eventData.SystemProperties["iothub-connection-device-id"];

                Console.WriteLine("Message Received, on partition: " +
                                  $"'{context.PartitionId}' " +
                                  $"device ID: '{deviceID}' " +
                                  $"data: '{data}' ");


            }

            return context.CheckpointAsync();
            
        }
    }
}
