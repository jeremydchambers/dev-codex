namespace AzureServiceBusPractice.Services;

public enum OrderProcessStatus
{
    Succeeded,
    /// <summary>
    /// Caller should throw so PeekLock abandons the message (retry → eventually DLQ).
    /// Includes validate failures and explicit simulateFailure demos.
    /// </summary>
    Failed
}
