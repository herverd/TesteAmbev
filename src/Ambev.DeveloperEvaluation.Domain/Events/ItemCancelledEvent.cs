using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event to notify when item of sale was cancelled.
/// </summary>
public class ItemCancelledEvent
{
    public required Guid CartId { get; init; }
    public required Guid CustomerId { get; init; }
    public required string CustomerName { get; init; }
    public required int TotalProducts { get; init; }
    public required decimal TotalAmount { get; init; }
    public required ICollection<SaleProduct> Products { get; init; }

    public static ItemCancelledEvent CreateFrom(Cart cart)
    {
        return new ItemCancelledEvent
        {
            CartId = cart.Id,
            CustomerId = cart.BoughtById,
            CustomerName = cart.BoughtBy.Username,
            TotalProducts = cart.Items.Count,
            TotalAmount = cart.TotalSaleAmount,
            Products = cart.Items
                .Where(i => i.PurchaseStatus is PurchaseStatus.Deleted)
                .Select(i => new SaleProduct
                {
                    ProductId = i.ProductId,
                    Title = i.Product.Title,
                    Price = i.Product.Price,
                    CancelledAt = i.CancelledAt ?? i.DeletedAt ?? DateTime.MinValue,
                    CancelledBy = i.CancelledBy?.Username ?? i.DeletedBy?.Username ?? string.Empty,
                    DiscountAmount = i.DiscountAmount,
                    DiscountPercent = i.DiscountPercentage,
                    TotalAmount = i.TotalAmount,
                })
                .ToArray(),
        };
    }

    public record SaleProduct
    {
        public required Guid ProductId { get; init; }
        public required string Title { get; init; }
        public required decimal Price { get; init; }
        public required DateTime CancelledAt { get; init; }
        public required string CancelledBy { get; init; }
        public required decimal DiscountPercent { get; init; }
        public required decimal DiscountAmount { get; init; }
        public required decimal TotalAmount { get; init; }
    };
}
