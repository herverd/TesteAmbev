using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Profile for mapping between Product entity and <see cref="CartResult"/>.
/// </summary>
public class GetCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCart operation.
    /// </summary>
    public GetCartProfile()
    {
        /*
        CreateMap<Cart, CartResult>()
            .ForMember(_ => _.UserId, mce => mce.MapFrom(_ => _.BoughtBy.Id))
            .ForMember(_ => _.Date, mce => mce.MapFrom(_ => _.SoldAt))
            .ForMember(_ => _.Branch, mce => mce.MapFrom(_ => _.StoreName))
            .ForMember(_ => _.IsCancelled, mce => mce.MapFrom((c, r) => c.PurchaseStatus is PurchaseStatus.Cancelled))
            .ForMember(_ => _.Products, mce => mce.MapFrom(_ => _.Items));
        */
    }
}
