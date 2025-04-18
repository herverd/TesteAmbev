using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.SearchPaginatedProductsByCategory;

/// <summary>
/// Handler for processing <see cref="SearchPaginatedProductsByCategoryCommand"/> requests.
/// </summary>
public class SearchPaginatedProductsByCategoryHandler : IRequestHandler<SearchPaginatedProductsByCategoryCommand, SearchPaginatedProductsByCategoryResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="SearchPaginatedProductsByCategoryHandler"/>.
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SearchPaginatedProductsByCategoryHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<SearchPaginatedProductsByCategoryResult> Handle(
        SearchPaginatedProductsByCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new SearchPaginatedProductsByCategoryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        PaginationQueryResult<Product> productsPaginationResult = await _productRepository.SearchPaginatedByCategoryNameAsync(request.CategoryName, request, cancellationToken);
        return _mapper.Map<SearchPaginatedProductsByCategoryResult>(productsPaginationResult);
    }
}
