namespace AzureServiceBusPractice.Services;

public sealed record EnqueueParseResult(bool Succeeded, string? MessageBody, string Summary);
