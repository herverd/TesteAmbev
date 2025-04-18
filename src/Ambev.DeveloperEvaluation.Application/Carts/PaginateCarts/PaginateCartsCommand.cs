using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.PaginateCarts;

/// <summary>
/// Command for paginate a list of carts.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for paging a list of carts.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CartResult"/>.
/// </remarks>
public class PaginateCartsCommand : PaginationQuery, IRequest<PaginateCartsResult>
{
}