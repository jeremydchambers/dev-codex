namespace AzureFunctionsPractice.Services;

public interface IOrderMessageProcessor
{
    OrderProcessResult Process(string messageBody);
}

public sealed record OrderProcessResult(bool Succeeded, string Summary);
