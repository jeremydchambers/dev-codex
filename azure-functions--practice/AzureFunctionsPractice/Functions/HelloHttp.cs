using AzureFunctionsPractice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsPractice.Functions;

public sealed class HelloHttp(
    IGreetingService greetingService,
    ILogger<HelloHttp> logger)
{
    [Function(nameof(HelloHttp))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "hello")]
        HttpRequest req)
    {
        var name = req.Query["name"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "world";
        }

        logger.LogInformation("HTTP HelloHttp invoked for {Name}", name);

        var message = greetingService.Greet(name);
        return new OkObjectResult(new { message });
    }
}
