using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace AzureServiceBusPractice.Functions;

public sealed class EnqueueOrderOutput
{
    [HttpResult]
    public required IActionResult HttpResponse { get; init; }

    [ServiceBusOutput("%OrdersQueueName%", Connection = "ServiceBusConnection")]
    public string? QueueMessage { get; init; }
}
