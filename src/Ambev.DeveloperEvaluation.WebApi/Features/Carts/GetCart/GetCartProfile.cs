using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// Profile for mapping between Application and API GetCart responses.
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart feature.
    /// </summary>
    public GetCartProfile()
    {
        CreateMap<GetCartRequest, GetCartCommand>();
        CreateMap<CartResult, CartResponse>();
    }
}
