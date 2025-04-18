using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IProductRepository"/> using Entity Framework Core.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of ProductRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        /// <inheritdoc/>
        public void Delete(Guid id)
        {
            var product = new Product { Id = id };
            _context.Products.Attach(product);
            _context.Products.Remove(product);
        }

        /// <inheritdoc/>
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Product?> GetByTitleAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(u => u.Title == name, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<ICollection<Product>> ListByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<PaginationQueryResult<Product>> PaginateAsync(
            PaginationQuery paging,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .ToPaginateAsync(paging, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<PaginationQueryResult<Product>> SearchPaginatedByCategoryNameAsync(
            string categoryName,
            PaginationQuery paging,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => EF.Functions.ILike(p.Category.Name, categoryName))
                .ToPaginateAsync(paging, cancellationToken);
        }
    }
}
