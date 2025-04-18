using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Handlers;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging.Consumers;

public class ItemCancelledConsumer : IHandleMessages<ItemCancelledEvent>
{
    private readonly ILogger _logger;

    public ItemCancelledConsumer(ILogger<ItemCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public Task Handle(ItemCancelledEvent message)
    {
        _logger.LogInformation($"Consuming ItemCancelledEvent: {JsonSerializer.Serialize(message)}");
        return Task.CompletedTask;
    }
}
