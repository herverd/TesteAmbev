using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ICategoryRepository"/> using Entity Framework Core.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">The database context</param>
        public CategoryRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void Create(Category category)
        {
            _context.Categories.Add(category);
        }

        /// <inheritdoc/>
        public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Name, $"%{name}%"), cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<ICollection<string>> ListAllCategoriesBeingUsedAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(c => c.Products.Any())
                .Select(c => c.Name)
                .ToArrayAsync();
        }
    }
}
