using AzureFunctionsPractice.Services;
using Microsoft.Extensions.Configuration;

namespace AzureFunctionsPractice.Tests;

public class GreetingServiceTests
{
    [Fact]
    public void Uses_GreetingPrefix_from_configuration()
    {
        var service = CreateService(("GreetingPrefix", "Howdy"));

        var message = service.Greet("Jeremy");

        Assert.Equal("Howdy, Jeremy!", message);
    }

    [Fact]
    public void Falls_back_to_Hello_when_prefix_missing()
    {
        var service = CreateService();

        var message = service.Greet("world");

        Assert.Equal("Hello, world!", message);
    }

    [Fact]
    public void Rejects_blank_name()
    {
        var service = CreateService(("GreetingPrefix", "Hi"));

        Assert.Throws<ArgumentException>(() => service.Greet("  "));
    }

    private static GreetingService CreateService(params (string Key, string Value)[] settings)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                settings.ToDictionary(pair => pair.Key, pair => (string?)pair.Value))
            .Build();

        return new GreetingService(configuration);
    }
}
