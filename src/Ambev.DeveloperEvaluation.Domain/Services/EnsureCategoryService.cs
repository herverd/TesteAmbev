using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class EnsureCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public EnsureCategoryService(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> EnsureCategoryNameAsync(string categoryName, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetByNameAsync(categoryName, cancellationToken);
            if (category is null)
            {
                category = new Category
                {
                    Name = categoryName,
                };
                _categoryRepository.Create(category);
            }

            return category;
        }
    }
}
