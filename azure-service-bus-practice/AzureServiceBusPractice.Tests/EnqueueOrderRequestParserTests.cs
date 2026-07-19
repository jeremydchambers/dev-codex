using AzureServiceBusPractice.Services;

namespace AzureServiceBusPractice.Tests;

public class EnqueueOrderRequestParserTests
{
    private readonly EnqueueOrderRequestParser _parser = new();

    [Fact]
    public void Accepts_valid_order_json()
    {
        var result = _parser.Parse("""{"orderId":"ORD-1","itemCount":2}""");

        Assert.True(result.Succeeded);
        Assert.Contains("ORD-1", result.MessageBody!, StringComparison.Ordinal);
        Assert.Contains("enqueue", result.Summary, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Rejects_empty_body()
    {
        var result = _parser.Parse("  ");

        Assert.False(result.Succeeded);
        Assert.Null(result.MessageBody);
        Assert.Contains("empty", result.Summary, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Rejects_invalid_json()
    {
        var result = _parser.Parse("not-json");

        Assert.False(result.Succeeded);
        Assert.Null(result.MessageBody);
        Assert.Contains("JSON", result.Summary, StringComparison.Ordinal);
    }

    [Fact]
    public void Rejects_missing_order_id()
    {
        var result = _parser.Parse("""{"itemCount":1}""");

        Assert.False(result.Succeeded);
        Assert.Null(result.MessageBody);
        Assert.Contains("OrderId", result.Summary, StringComparison.Ordinal);
    }

    [Fact]
    public void Preserves_simulateFailure_flag_in_message_body()
    {
        var result = _parser.Parse("""{"orderId":"ORD-POISON","itemCount":1,"simulateFailure":true}""");

        Assert.True(result.Succeeded);
        Assert.Contains("simulateFailure", result.MessageBody!, StringComparison.OrdinalIgnoreCase);
    }
}
