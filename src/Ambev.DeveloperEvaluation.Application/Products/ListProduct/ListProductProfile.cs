using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Profile for mapping between Product entity and <see cref="ListProductResult"/>.
/// </summary>
public class ListProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProduct operation.
    /// </summary>
    public ListProductProfile()
    {
        CreateMap<PaginationQueryResult<Product>, ListProductResult>();
        CreateMap<Product, ProductResult>();
    }
}
