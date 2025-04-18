using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

/// <summary>
/// Profile for mapping between Application and API DeleteCart responses.
/// </summary>
public class DeleteCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCart feature.
    /// </summary>
    public DeleteCartProfile()
    {
        CreateMap<DeleteCartRequest, DeleteCartCommand>();
    }
}
