using AzureServiceBusPractice.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<IEnqueueOrderRequestParser, EnqueueOrderRequestParser>();
builder.Services.AddSingleton<IOrderMessageProcessor, OrderMessageProcessor>();

builder.Build().Run();
