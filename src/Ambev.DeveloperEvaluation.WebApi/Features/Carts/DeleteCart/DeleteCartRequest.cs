using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Request model for deleting a cart
/// </summary>
public class DeleteCartRequest
{
    /// <summary>
    /// The unique identifier of the cart to delete
    /// </summary>
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}
