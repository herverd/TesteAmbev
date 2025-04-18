namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Represents a request to create a new product in the system.
/// </summary>
public class UpdateCartRequest
{
    /// <summary>
    /// The unique identifier of the product to update.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets a customer who bought a cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets a date of sale.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets a branch where the sale was made
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Gets products in the cart.
    /// </summary>
    public ICollection<UpdateCartItemRequest> Products { get; set; } = [];

    /// <summary>
    /// Associate id to request.
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Instance <see cref="UpdateCartRequest"/> of request.</returns>
    public UpdateCartRequest WithId(Guid id)
    {
        Id = id;
        return this;
    }
}
