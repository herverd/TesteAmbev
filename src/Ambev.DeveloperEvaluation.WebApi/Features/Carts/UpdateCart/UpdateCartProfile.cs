using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Profile for mapping between Application and API UpdateCart responses.
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart feature.
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>();
        CreateMap<CartResult, CartResponse>();
        CreateMap<UpdateCartItemRequest, UpdateCartItem>();
    }
}
