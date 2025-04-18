using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Messaging;

/// <summary>
/// Responsable to send event message.
/// </summary>
public class RebusMessageProducer : IEventNotification
{
    private readonly IBus _bus;
    private readonly ILogger _logger;

    public RebusMessageProducer(
        IBus bus,
        ILogger<RebusMessageProducer> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task NotifyAsync<T>(T messageEvent)
    {
        _logger.LogInformation($"Publish event of type {typeof(T).Name} => {JsonSerializer.Serialize(messageEvent)}");
        await _bus.Publish(messageEvent);
    }
}
