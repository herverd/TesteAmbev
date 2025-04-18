using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// API response model for a single Product.
/// </summary>
public class ProductResponse
{
    /// <summary>
    /// The unique identifier of the created product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the product's title.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets the product's full price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets the product's cover image.
    /// </summary>
    public string Image { get; set; } = default!;

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public Rating Rating { get; set; } = default!;

    /// <summary>
    /// Gets the product's category name.
    /// </summary>
    public string Category { get; private set; } = default!;
}
