using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.PaginateCarts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.PaginateCarts;

/// <summary>
/// Profile for mapping between Application and API PaginateCarts responses.
/// </summary>
public class PaginateCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for PaginateCarts feature.
    /// </summary>
    public PaginateCartsProfile()
    {
        CreateMap<PaginateCartsRequest, PaginateCartsCommand>();
        CreateMap<CartResult, CartResponse>();
        CreateMap<CartItemResult, CartItemResponse>();
    }
}
