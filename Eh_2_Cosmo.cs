using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Microsoft.Azure.EventHubs; deprecated
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

namespace Eh_Cosmo_IO
{
    public static class Eh_2_Cosmo
    {
        [Function("Eh_2_Cosmo")]
        public static async Task Run([EventHubTrigger("eh-steaming-data", Connection = "eh-built-in_workplace-safety-east2_IOTHUB", ConsumerGroup="cosmodb")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    // string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.ToArray());

                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    await Task.Yield();
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
