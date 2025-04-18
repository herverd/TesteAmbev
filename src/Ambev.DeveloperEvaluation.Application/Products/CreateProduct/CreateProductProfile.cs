using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between <see cref="Product"/> entity and <see cref="CreateProductResult"/>.
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(_ => _.StockQuantity, mce => mce.MapFrom(_ => _.Quantity));
        CreateMap<Product, ProductResult>();
    }
}
