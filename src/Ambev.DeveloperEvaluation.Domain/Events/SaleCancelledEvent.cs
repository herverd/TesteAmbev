using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event to notify when sale was cancelled.
/// </summary>
public class SaleCancelledEvent
{
    public required Guid CartId { get; init; }
    public required Guid CustomerId { get; init; }
    public required string CustomerName { get; init; }
    public required DateTime CancelledAt { get; init; }
    public required string CancelledBy { get; init; }
    public required int TotalProducts { get; init; }
    public required decimal TotalAmount { get; init; }

    public static SaleCancelledEvent CreateFrom(Cart cart)
    {
        return new SaleCancelledEvent
        {
            CartId = cart.Id,
            CustomerId = cart.BoughtById,
            CustomerName = cart.BoughtBy.Username,
            CancelledAt = cart.CancelledAt.GetValueOrDefault(),
            CancelledBy = cart.CancelledBy?.Username ?? string.Empty,
            TotalProducts = cart.Items.Count,
            TotalAmount = cart.TotalSaleAmount,
        };
    }
}
