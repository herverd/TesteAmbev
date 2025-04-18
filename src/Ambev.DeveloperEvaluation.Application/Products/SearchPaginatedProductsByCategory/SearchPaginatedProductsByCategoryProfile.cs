using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.SearchPaginatedProductsByCategory;

/// <summary>
/// Profile for mapping between Product entity and <see cref="SearchPaginatedProductsByCategoryResult"/>.
/// </summary>
public class SearchPaginatedProductsByCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProduct operation.
    /// </summary>
    public SearchPaginatedProductsByCategoryProfile()
    {
        CreateMap<PaginationQueryResult<Product>, SearchPaginatedProductsByCategoryResult>();
        CreateMap<Product, ProductResult>();
    }
}
