using AzureServiceBusPractice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusPractice.Functions;

public sealed class EnqueueOrderHttp(
    EnqueueOrderRequestParser parser,
    ILogger<EnqueueOrderHttp> logger)
{
    [Function(nameof(EnqueueOrderHttp))]
    public async Task<EnqueueOrderOutput> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orders")]
        HttpRequest req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var parsed = parser.Parse(body);

        if (!parsed.Succeeded || parsed.MessageBody is null)
        {
            logger.LogWarning("Enqueue rejected: {Summary}", parsed.Summary);
            return new EnqueueOrderOutput
            {
                HttpResponse = new BadRequestObjectResult(new { error = parsed.Summary })
            };
        }

        logger.LogInformation("Enqueue accepted: {Summary}", parsed.Summary);

        return new EnqueueOrderOutput
        {
            HttpResponse = new AcceptedResult(
                location: null,
                value: new { status = "enqueued", summary = parsed.Summary }),
            QueueMessage = parsed.MessageBody
        };
    }
}
