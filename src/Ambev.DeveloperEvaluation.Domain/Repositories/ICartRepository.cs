using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for <see cref="Cart"/> entity operations.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Creates a new cart in the repository.
        /// </summary>
        /// <param name="cart">The cart to create</param>
        void Create(Cart cart);

        /// <summary>
        /// Retrieves a cart by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found, null otherwise</returns>
        Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a cart by their unique identifier with active items.
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found, null otherwise</returns>
        Task<Cart?> GetByIdWithActiveItemsAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all paginated carts.
        /// </summary>
        /// <param name="paging">Info to paginate</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The list of paginated carts</returns>
        Task<PaginationQueryResult<Cart>> PaginateAsync(
            PaginationQuery paging,
            CancellationToken cancellationToken = default);
    }
}
