using System.Text.Json;

namespace AzureServiceBusPractice.Services;

/// <summary>
/// Shared JSON read/write for <see cref="OrderMessageDocument"/>.
/// </summary>
public static class OrderMessageJson
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static OrderMessageReadResult Read(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new OrderMessageReadResult(false, null, "Message body is empty.");
        }

        OrderMessageDto? dto;
        try
        {
            dto = JsonSerializer.Deserialize<OrderMessageDto>(json, JsonOptions);
        }
        catch (JsonException)
        {
            return new OrderMessageReadResult(false, null, "Message body is not valid JSON.");
        }

        if (dto is null || string.IsNullOrWhiteSpace(dto.OrderId))
        {
            return new OrderMessageReadResult(false, null, "Message is missing required OrderId.");
        }

        var document = new OrderMessageDocument(
            dto.OrderId.Trim(),
            dto.ItemCount is > 0 ? dto.ItemCount.Value : 0,
            dto.SimulateFailure);

        return new OrderMessageReadResult(true, document, string.Empty);
    }

    public static string Write(OrderMessageDocument document) =>
        JsonSerializer.Serialize(new OrderMessageDto
        {
            OrderId = document.OrderId,
            ItemCount = document.ItemCount,
            SimulateFailure = document.SimulateFailure
        });

    private sealed class OrderMessageDto
    {
        public string? OrderId { get; set; }
        public int? ItemCount { get; set; }
        public bool SimulateFailure { get; set; }
    }
}
