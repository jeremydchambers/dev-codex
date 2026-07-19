using AzureFunctionsPractice.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsPractice.Functions;

public sealed class ProcessOrderMessage(
    IOrderMessageProcessor processor,
    ILogger<ProcessOrderMessage> logger)
{
    /// <summary>
    /// Service Bus queue trigger. Queue name comes from app setting OrdersQueueName.
    /// Connection uses identity-based settings in Azure (ServiceBusConnection__fullyQualifiedNamespace)
    /// or a connection string locally (ServiceBusConnection).
    /// </summary>
    [Function(nameof(ProcessOrderMessage))]
    public void Run(
        [ServiceBusTrigger("%OrdersQueueName%", Connection = "ServiceBusConnection")]
        string messageBody)
    {
        var result = processor.Process(messageBody);

        if (result.Succeeded)
        {
            logger.LogInformation("Service Bus order processed: {Summary}", result.Summary);
        }
        else
        {
            logger.LogWarning("Service Bus order rejected: {Summary}", result.Summary);
        }
    }
}
