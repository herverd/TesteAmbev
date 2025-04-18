using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging.Consumers;

public class SaleCancelledConsumer : IHandleMessages<SaleCancelledEvent>
{
    private readonly ILogger _logger;

    public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCancelledEvent message)
    {
        _logger.LogInformation($"Consuming SaleCancelledEvent: {JsonSerializer.Serialize(message)}");
        return Task.CompletedTask;
    }
}
