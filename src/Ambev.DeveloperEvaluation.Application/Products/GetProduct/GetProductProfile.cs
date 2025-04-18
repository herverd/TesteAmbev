using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Profile for mapping between Product entity and <see cref="ProductResult"/>.
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProduct operation.
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, ProductResult>();
    }
}
