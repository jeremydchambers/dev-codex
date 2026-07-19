namespace AzureServiceBusPractice.Services;

public sealed record OrderProcessResult(OrderProcessStatus Status, string Summary);
