using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API UpdateProduct responses.
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateProduct feature.
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ForMember(c => c.CategoryName, opt => opt.MapFrom(s => s.Category));
        CreateMap<ProductResult, ProductResponse>()
            .ForMember(c => c.Category, opt => opt.MapFrom(s => s.CategoryName));
    }
}
