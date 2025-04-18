using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Request model for getting a product by ID.
/// </summary>
public class GetCartRequest
{
    /// <summary>
    /// The unique identifier of the product to retrieve.
    /// </summary>
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}
