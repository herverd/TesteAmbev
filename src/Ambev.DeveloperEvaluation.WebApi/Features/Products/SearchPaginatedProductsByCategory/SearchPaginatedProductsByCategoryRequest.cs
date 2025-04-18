using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.SearchPaginatedProductsByCategory;

/// <summary>
/// Request model for searching paginated products by category.
/// </summary>
public class SearchPaginatedProductsByCategoryRequest : PaginatedRequest
{
    [FromRoute(Name = "categoryName")]
    public required string CategoryName { get; init; }
}
