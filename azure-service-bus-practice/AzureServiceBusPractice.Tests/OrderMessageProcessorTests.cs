using AzureServiceBusPractice.Services;

namespace AzureServiceBusPractice.Tests;

public class OrderMessageProcessorTests
{
    private readonly OrderMessageProcessor _processor = new();

    [Fact]
    public void Succeeds_for_valid_order()
    {
        var result = _processor.Process("""{"orderId":"ORD-1","itemCount":3}""");

        Assert.Equal(OrderProcessStatus.Succeeded, result.Status);
        Assert.Contains("ORD-1", result.Summary, StringComparison.Ordinal);
        Assert.Contains("3", result.Summary, StringComparison.Ordinal);
    }

    [Fact]
    public void Marks_simulateFailure_for_retry_and_dlq_path()
    {
        var result = _processor.Process("""{"orderId":"ORD-POISON","simulateFailure":true}""");

        Assert.Equal(OrderProcessStatus.Failed, result.Status);
        Assert.Contains("simulateFailure", result.Summary, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Rejects_empty_body_as_failure()
    {
        var result = _processor.Process("   ");

        Assert.Equal(OrderProcessStatus.Failed, result.Status);
        Assert.Contains("empty", result.Summary, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Rejects_invalid_json_as_failure()
    {
        var result = _processor.Process("not-json");

        Assert.Equal(OrderProcessStatus.Failed, result.Status);
        Assert.Contains("JSON", result.Summary, StringComparison.Ordinal);
    }

    [Fact]
    public void Rejects_missing_order_id_as_failure()
    {
        var result = _processor.Process("""{"itemCount":1}""");

        Assert.Equal(OrderProcessStatus.Failed, result.Status);
        Assert.Contains("OrderId", result.Summary, StringComparison.Ordinal);
    }
}
