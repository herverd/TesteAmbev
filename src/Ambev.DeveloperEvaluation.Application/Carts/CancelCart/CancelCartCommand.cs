using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CancelCart;

/// <summary>
/// Command for cancel a cart.
/// </summary>
public record CancelCartCommand : IRequest<CancelCartResponse>
{
    /// <summary>
    /// The unique identifier of the cart to cancel.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="CancelCartResponse"/>.
    /// </summary>
    /// <param name="id">The ID of the cart to cancel</param>
    public CancelCartCommand(Guid id)
    {
        Id = id;
    }
}
