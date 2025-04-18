using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">The database context</param>
        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Create(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        /// <inheritdoc/>
        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(p => p.Items.Where(i => i.PurchaseStatus != PurchaseStatus.Deleted))
                    .ThenInclude(i => i.Product)
                .Where(c => c.PurchaseStatus != PurchaseStatus.Deleted)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<Cart?> GetByIdWithActiveItemsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(p => p.Items.Where(i => i.PurchaseStatus == PurchaseStatus.Created))
                    .ThenInclude(i => i.Product)
                .Where(c => c.PurchaseStatus != PurchaseStatus.Deleted)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<PaginationQueryResult<Cart>> PaginateAsync(
            PaginationQuery paging,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<Cart, IEnumerable<CartItem>>> itemsNotDeleted = p => p.Items.Where(i => i.PurchaseStatus != PurchaseStatus.Deleted);

            // note: ordering is applied only to the items of the cart. Other orderings should be done manually and used in the Include method.
            var sortsByRelatedItems = paging.Orders.Where(s => s.Key.StartsWith(nameof(Cart.Items) + '.', StringComparison.OrdinalIgnoreCase));
            itemsNotDeleted = itemsNotDeleted.RewriteExpressionWithOrderBy(sortsByRelatedItems);

            return await _context.Carts
                .AsNoTracking()
                .Include(itemsNotDeleted)
                .Where(c => c.PurchaseStatus != PurchaseStatus.Deleted)
                .ToPaginateAsync(paging, cancellationToken);
        }
    }
}
