namespace AzureServiceBusPractice.Services;

/// <summary>
/// Validates and summarizes a Service Bus order message body.
/// Pure logic — the function class owns the trigger and whether to throw (abandon → DLQ).
///
/// Idempotency checklist (at-least-once): prefer a business key (OrderId) / MessageId;
/// record "already processed" before side effects, or make side effects idempotent;
/// treat redelivery as normal. This demo does not persist processed ids.
/// </summary>
public sealed class OrderMessageProcessor
{
    public OrderProcessResult Process(string messageBody)
    {
        var read = OrderMessageJson.Read(messageBody);
        if (!read.Succeeded || read.Document is null)
        {
            return new OrderProcessResult(OrderProcessStatus.Failed, read.Error);
        }

        if (read.Document.SimulateFailure)
        {
            return new OrderProcessResult(
                OrderProcessStatus.Failed,
                $"Simulated failure for order '{read.Document.OrderId}' (simulateFailure=true).");
        }

        return new OrderProcessResult(
            OrderProcessStatus.Succeeded,
            $"Accepted order '{read.Document.OrderId}' with {read.Document.ItemCount} item(s).");
    }
}
