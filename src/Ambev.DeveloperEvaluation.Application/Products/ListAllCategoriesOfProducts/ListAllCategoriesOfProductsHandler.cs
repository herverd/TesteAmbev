using Ambev.DeveloperEvaluation.Application.Products.ListAllCategoriesOfProducts;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListAllCategoriesOfProduct;

/// <summary>
/// Handler for processing <see cref="ListAllCategoriesOfProductsCommand"/> requests.
/// </summary>
public class ListAllCategoriesOfProductsHandler : IRequestHandler<ListAllCategoriesOfProductsCommand, ListAllCategoriesOfProductsResult>
{
    private readonly ICategoryRepository _categoryRepository;

    public ListAllCategoriesOfProductsHandler(
        ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ListAllCategoriesOfProductsResult> Handle(
        ListAllCategoriesOfProductsCommand request,
        CancellationToken cancellationToken)
    {
        var categoryNames = await _categoryRepository.ListAllCategoriesBeingUsedAsync(cancellationToken);
        var result = new ListAllCategoriesOfProductsResult();
        result.AddRange(categoryNames);
        return result;
    }
}
