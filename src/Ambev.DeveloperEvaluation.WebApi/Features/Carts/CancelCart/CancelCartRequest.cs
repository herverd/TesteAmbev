using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelCart;

/// <summary>
/// Request model for cancellation a cart
/// </summary>
public class CancelCartRequest
{
    /// <summary>
    /// The unique identifier of the cart to cancel
    /// </summary>
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}
