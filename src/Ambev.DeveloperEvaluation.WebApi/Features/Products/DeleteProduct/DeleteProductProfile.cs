using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API DeleteProduct responses.
/// </summary>
public class DeleteProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteProduct feature.
    /// </summary>
    public DeleteProductProfile()
    {
        CreateMap<DeleteProductRequest, DeleteProductCommand>();
    }
}
