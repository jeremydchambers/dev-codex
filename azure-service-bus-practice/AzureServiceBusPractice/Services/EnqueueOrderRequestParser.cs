using System.Text.Json;
using System.Text.Json.Nodes;

namespace AzureServiceBusPractice.Services;

/// <summary>
/// Validates an HTTP enqueue body and returns the canonical JSON to put on the queue.
/// </summary>
public sealed class EnqueueOrderRequestParser : IEnqueueOrderRequestParser
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public EnqueueParseResult Parse(string requestBody)
    {
        if (string.IsNullOrWhiteSpace(requestBody))
        {
            return new EnqueueParseResult(false, null, "Request body is empty.");
        }

        JsonNode? node;
        try
        {
            node = JsonNode.Parse(requestBody);
        }
        catch (JsonException)
        {
            return new EnqueueParseResult(false, null, "Request body is not valid JSON.");
        }

        if (node is not JsonObject obj)
        {
            return new EnqueueParseResult(false, null, "Request body must be a JSON object.");
        }

        OrderEnqueueRequest? order;
        try
        {
            order = obj.Deserialize<OrderEnqueueRequest>(JsonOptions);
        }
        catch (JsonException)
        {
            return new EnqueueParseResult(false, null, "Request body is not valid JSON.");
        }

        if (order is null || string.IsNullOrWhiteSpace(order.OrderId))
        {
            return new EnqueueParseResult(false, null, "Message is missing required OrderId.");
        }

        var canonical = new OrderEnqueueRequest
        {
            OrderId = order.OrderId.Trim(),
            ItemCount = order.ItemCount is > 0 ? order.ItemCount : 0,
            SimulateFailure = order.SimulateFailure
        };

        var messageBody = JsonSerializer.Serialize(canonical);
        return new EnqueueParseResult(
            true,
            messageBody,
            $"Ready to enqueue order '{canonical.OrderId}'.");
    }

    private sealed class OrderEnqueueRequest
    {
        public string? OrderId { get; set; }
        public int? ItemCount { get; set; }
        public bool SimulateFailure { get; set; }
    }
}
