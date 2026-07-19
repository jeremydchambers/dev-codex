using Microsoft.Extensions.Configuration;

namespace AzureFunctionsPractice.Services;

/// <summary>
/// Formats a greeting using an app setting (GreetingPrefix).
/// Kept free of Functions bindings so unit tests don't need the host.
/// </summary>
public sealed class GreetingService(IConfiguration configuration) : IGreetingService
{
    public string Greet(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var prefix = configuration["GreetingPrefix"];
        if (string.IsNullOrWhiteSpace(prefix))
        {
            prefix = "Hello";
        }

        return $"{prefix.Trim()}, {name.Trim()}!";
    }
}
