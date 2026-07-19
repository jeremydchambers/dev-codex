namespace AzureServiceBusPractice.Services;

public interface IOrderMessageProcessor
{
    OrderProcessResult Process(string messageBody);
}

public enum OrderProcessStatus
{
    Succeeded,
    /// <summary>
    /// Caller should throw so PeekLock abandons the message (retry → eventually DLQ).
    /// Includes validate failures and explicit simulateFailure demos.
    /// </summary>
    Failed
}

public sealed record OrderProcessResult(OrderProcessStatus Status, string Summary);
