using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Request model for deleting a product
/// </summary>
public class DeleteProductRequest
{
    /// <summary>
    /// The unique identifier of the product to delete
    /// </summary>
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}
