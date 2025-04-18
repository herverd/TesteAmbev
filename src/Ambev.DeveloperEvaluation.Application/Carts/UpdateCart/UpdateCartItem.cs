namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Represents command the item of cart.
/// </summary>
public class UpdateCartItem
{
    /// <summary>
    /// Gets the product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the quantity of products.
    /// </summary>
    public int Quantity { get; set; }
}
