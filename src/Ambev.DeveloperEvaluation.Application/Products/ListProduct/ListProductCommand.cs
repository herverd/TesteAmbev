using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct
{
    /// <summary>
    /// Command for retrieving a product list.
    /// </summary>
    public class ListProductCommand : PaginationQuery, IRequest<ListProductResult>
    {
    }
}
