using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.SearchPaginatedProductsByCategory;

/// <summary>
/// Command for retrieving a product list by category name.
/// </summary>
public class SearchPaginatedProductsByCategoryCommand : PaginationQuery, IRequest<SearchPaginatedProductsByCategoryResult>
{
    /// <summary>
    /// Category name.
    /// </summary>
    public required string CategoryName { get; set; }
}
