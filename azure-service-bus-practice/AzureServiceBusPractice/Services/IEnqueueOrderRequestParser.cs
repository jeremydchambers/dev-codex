namespace AzureServiceBusPractice.Services;

public interface IEnqueueOrderRequestParser
{
    EnqueueParseResult Parse(string requestBody);
}

public sealed record EnqueueParseResult(bool Succeeded, string? MessageBody, string Summary);
