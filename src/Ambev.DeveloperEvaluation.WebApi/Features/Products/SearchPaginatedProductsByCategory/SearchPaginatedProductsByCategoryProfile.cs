using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.SearchPaginatedProductsByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.SearchPaginatedProductsByCategory;

/// <summary>
/// Profile for mapping between Application and API SearchPaginatedProductsByCategory responses.
/// </summary>
public class SearchPaginatedProductsByCategoryProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for SearchPaginatedProductsByCategory feature.
    /// </summary>
    public SearchPaginatedProductsByCategoryProfile()
    {
        CreateMap<SearchPaginatedProductsByCategoryRequest, SearchPaginatedProductsByCategoryCommand>();
        CreateMap<ProductResult, ProductResponse>()
            .ForMember(c => c.Category, opt => opt.MapFrom(s => s.CategoryName));
    }
}
