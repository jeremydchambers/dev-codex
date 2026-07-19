using System.Text.Json;

namespace AzureFunctionsPractice.Services;

/// <summary>
/// Validates and summarizes a Service Bus order message body.
/// Pure logic — the function class only owns the trigger and logging.
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
            return new OrderProcessResult(false, "Message body is empty.");
        }

        OrderMessage? order;
        try
        {
            order = JsonSerializer.Deserialize<OrderMessage>(messageBody, JsonOptions);
        }
        catch (JsonException)
        {
            return new OrderProcessResult(false, "Message body is not valid JSON.");
        }

        if (order is null || string.IsNullOrWhiteSpace(order.OrderId))
        {
            return new OrderProcessResult(false, "Message is missing required OrderId.");
        }

        var itemCount = order.ItemCount is > 0 ? order.ItemCount.Value : 0;
        return new OrderProcessResult(
            true,
            $"Accepted order '{order.OrderId.Trim()}' with {itemCount} item(s).");
    }

    private sealed class OrderMessage
    {
        public string? OrderId { get; set; }
        public int? ItemCount { get; set; }
    }
}
