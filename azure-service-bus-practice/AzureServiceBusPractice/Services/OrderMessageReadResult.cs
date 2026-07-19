namespace AzureServiceBusPractice.Services;

public sealed record OrderMessageReadResult(
    bool Succeeded,
    OrderMessageDocument? Document,
    string Error);
