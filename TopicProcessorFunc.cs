using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.TopicProcessorFunc
{
    public class TopicProcessorFunc
    {
        private readonly ILogger<TopicProcessorFunc> _logger;

        public TopicProcessorFunc(ILogger<TopicProcessorFunc> logger)
        {
            _logger = logger;
        }

        [Function(nameof(TopicProcessorFunc))]
        public async Task Run(
            [ServiceBusTrigger("topicfiltersampletopic", "ColorBlueSize10Orders", Connection = "mcservicebusaz_SERVICEBUS")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
