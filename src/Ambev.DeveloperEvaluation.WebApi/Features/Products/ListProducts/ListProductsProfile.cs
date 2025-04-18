using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

/// <summary>
/// Profile for mapping between Application and API ListProduct responses.
/// </summary>
public class ListProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for ListProduct feature.
    /// </summary>
    public ListProductsProfile()
    {
        CreateMap<ListProductsRequest, ListProductCommand>();
        CreateMap<ProductResult, ProductResponse>()
            .ForMember(c => c.Category, opt => opt.MapFrom(s => s.CategoryName));
    }
}
