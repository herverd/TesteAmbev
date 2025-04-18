using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API GetProduct responses.
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct feature.
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<GetProductRequest, GetProductCommand>();
        CreateMap<ProductResult, ProductResponse>()
            .ForMember(c => c.Category, opt => opt.MapFrom(s => s.CategoryName));
    }
}
