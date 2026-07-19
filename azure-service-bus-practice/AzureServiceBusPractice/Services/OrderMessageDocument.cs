namespace AzureServiceBusPractice.Services;

/// <summary>
/// Canonical order payload shared by enqueue and consume paths.
/// </summary>
public sealed record OrderMessageDocument(string OrderId, int ItemCount, bool SimulateFailure);
