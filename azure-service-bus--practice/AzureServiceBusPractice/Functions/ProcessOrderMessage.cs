using AzureServiceBusPractice.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusPractice.Functions;

public sealed class ProcessOrderMessage(
    OrderMessageProcessor processor,
    ILogger<ProcessOrderMessage> logger)
{
    /// <summary>
    /// Service Bus queue trigger (PeekLock-style). Queue name from OrdersQueueName.
    /// Throwing abandons the message so it can retry and eventually dead-letter.
    /// </summary>
    [Function(nameof(ProcessOrderMessage))]
    public void Run(
        [ServiceBusTrigger("%OrdersQueueName%", Connection = "ServiceBusConnection")]
        string messageBody)
    {
        var result = processor.Process(messageBody);

        if (result.Status == OrderProcessStatus.Failed)
        {
            logger.LogWarning("Service Bus order will abandon for retry/DLQ: {Summary}", result.Summary);
            throw new InvalidOperationException(result.Summary);
        }

        logger.LogInformation("Service Bus order processed: {Summary}", result.Summary);
    }
}
