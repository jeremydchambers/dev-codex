namespace AzureServiceBusPractice.Services;

/// <summary>
/// Validates an HTTP enqueue body and returns the canonical JSON to put on the queue.
/// </summary>
public sealed class EnqueueOrderRequestParser
{
    public EnqueueParseResult Parse(string requestBody)
    {
        if (string.IsNullOrWhiteSpace(requestBody))
        {
            return new EnqueueParseResult(false, null, "Request body is empty.");
        }

        var read = OrderMessageJson.Read(requestBody);
        if (!read.Succeeded || read.Document is null)
        {
            var summary = read.Error.Replace("Message body", "Request body", StringComparison.Ordinal);
            return new EnqueueParseResult(false, null, summary);
        }

        var messageBody = OrderMessageJson.Write(read.Document);
        return new EnqueueParseResult(
            true,
            messageBody,
            $"Ready to enqueue order '{read.Document.OrderId}'.");
    }
}
