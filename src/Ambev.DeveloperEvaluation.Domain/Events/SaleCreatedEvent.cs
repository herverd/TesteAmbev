using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Event to notify when sale was created.
/// </summary>
public class SaleCreatedEvent
{
    public required Guid CartId { get; init; }
    public required Guid CustomerId { get; init; }
    public required string CustomerName { get; init; }
    public required long SaleNumber { get; init; }
    public required string StoreName { get; init; }
    public required int TotalProducts { get; init; }
    public required decimal TotalAmount { get; init; }
    public required ICollection<SaleProduct> Products { get; init; }

    public static SaleCreatedEvent CreateFrom(Cart cart)
    {
        return new SaleCreatedEvent
        {
            CartId = cart.Id,
            CustomerId = cart.BoughtById,
            CustomerName = cart.BoughtBy.Username,
            SaleNumber = cart.SaleNumber,
            StoreName = cart.StoreName,
            TotalProducts = cart.Items.Count,
            TotalAmount = cart.TotalSaleAmount,
            Products = cart.Items.Select(i => new SaleProduct
            {
                ProductId = i.ProductId,
                Title = i.Product.Title,
                Price = i.Product.Price,
                DiscountAmount = i.DiscountAmount,
                DiscountPercent = i.DiscountPercentage,
                TotalAmount = i.TotalAmount,
            }).ToArray()
        };
    }

    public record SaleProduct
    {
        public required Guid ProductId { get; init; }
        public required string Title { get; init; }
        public required decimal Price { get; init; }
        public required decimal DiscountPercent { get; init; }
        public required decimal DiscountAmount { get; init; }
        public required decimal TotalAmount { get; init; }
    };
}
