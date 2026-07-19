using System.Text.Json;

namespace AzureServiceBusPractice.Services;

/// <summary>
/// Validates and summarizes a Service Bus order message body.
/// Pure logic — the function class owns the trigger and whether to throw (abandon → DLQ).
///
/// Idempotency checklist (at-least-once): prefer a business key (OrderId) / MessageId;
/// record "already processed" before side effects, or make side effects idempotent;
/// treat redelivery as normal. This demo does not persist processed ids.
/// </summary>
public sealed class OrderMessageProcessor : IOrderMessageProcessor
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public OrderProcessResult Process(string messageBody)
    {
        if (string.IsNullOrWhiteSpace(messageBody))
        {
            return new OrderProcessResult(
                OrderProcessStatus.Failed,
                "Message body is empty.");
        }

        OrderMessage? order;
        try
        {
            order = JsonSerializer.Deserialize<OrderMessage>(messageBody, JsonOptions);
        }
        catch (JsonException)
        {
            return new OrderProcessResult(
                OrderProcessStatus.Failed,
                "Message body is not valid JSON.");
        }

        if (order is null || string.IsNullOrWhiteSpace(order.OrderId))
        {
            return new OrderProcessResult(
                OrderProcessStatus.Failed,
                "Message is missing required OrderId.");
        }

        if (order.SimulateFailure)
        {
            return new OrderProcessResult(
                OrderProcessStatus.Failed,
                $"Simulated failure for order '{order.OrderId.Trim()}' (simulateFailure=true).");
        }

        var itemCount = order.ItemCount is > 0 ? order.ItemCount.Value : 0;
        return new OrderProcessResult(
            OrderProcessStatus.Succeeded,
            $"Accepted order '{order.OrderId.Trim()}' with {itemCount} item(s).");
    }

    private sealed class OrderMessage
    {
        public string? OrderId { get; set; }
        public int? ItemCount { get; set; }
        public bool SimulateFailure { get; set; }
    }
}
