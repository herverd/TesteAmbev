using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Product entity operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Creates a new product in the repository.
        /// </summary>
        /// <param name="product">The product to create</param>
        void Create(Product product);

        /// <summary>
        /// Deletes a product from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        void Delete(Guid id);

        /// <summary>
        /// Retrieves a product by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a product by their name.
        /// </summary>
        /// <param name="title">The title to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>
        Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve a list of products by your ids.
        /// </summary>
        /// <param name="ids">List of identifiers of product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of product contains your ids.</returns>
        Task<ICollection<Product>> ListByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all paginated products.
        /// </summary>
        /// <param name="paging">Info to paginate</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The list of paginated products</returns>
        Task<PaginationQueryResult<Product>> PaginateAsync(
            PaginationQuery paging,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all paginated products searched by category name.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="paging">Info to paginate</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task<PaginationQueryResult<Product>> SearchPaginatedByCategoryNameAsync(
            string categoryName,
            PaginationQuery paging,
            CancellationToken cancellationToken = default);
    }
}
