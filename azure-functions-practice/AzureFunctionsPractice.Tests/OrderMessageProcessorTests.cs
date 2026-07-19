using AzureFunctionsPractice.Services;

namespace AzureFunctionsPractice.Tests;

public class OrderMessageProcessorTests
{
    private readonly OrderMessageProcessor _processor = new();

    [Fact]
    public void Accepts_valid_order_json()
    {
        var result = _processor.Process("""{"orderId":"ORD-1","itemCount":3}""");

        Assert.True(result.Succeeded);
        Assert.Contains("ORD-1", result.Summary, StringComparison.Ordinal);
        Assert.Contains("3", result.Summary, StringComparison.Ordinal);
    }

    [Fact]
    public void Rejects_empty_body()
    {
        var result = _processor.Process("   ");

        Assert.False(result.Succeeded);
        Assert.Contains("empty", result.Summary, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Rejects_invalid_json()
    {
        var result = _processor.Process("not-json");

        Assert.False(result.Succeeded);
        Assert.Contains("JSON", result.Summary, StringComparison.Ordinal);
    }

    [Fact]
    public void Rejects_missing_order_id()
    {
        var result = _processor.Process("""{"itemCount":1}""");

        Assert.False(result.Succeeded);
        Assert.Contains("OrderId", result.Summary, StringComparison.Ordinal);
    }
}
