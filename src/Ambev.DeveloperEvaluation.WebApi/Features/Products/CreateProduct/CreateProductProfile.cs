using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses.
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct feature.
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(c => c.CategoryName, opt => opt.MapFrom(s => s.Category));
        CreateMap<ProductResult, ProductResponse>()
            .ForMember(c => c.Category, opt => opt.MapFrom(s => s.CategoryName));
    }
}
