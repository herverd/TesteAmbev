using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Profile for mapping between <see cref="Cart"/> entity and <see cref="CreateCartResult"/>.
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduct operation
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<Cart, CartResult>()
            .ForMember(_ => _.UserId, mce => mce.MapFrom(_ => _.BoughtById))
            .ForMember(_ => _.Date, mce => mce.MapFrom(_ => _.SoldAt))
            .ForMember(_ => _.Branch, mce => mce.MapFrom(_ => _.StoreName))
            .ForMember(_ => _.Cancelled, mce => mce.MapFrom(new CardCancelledResolver(), c => c.PurchaseStatus))
            .ForMember(_ => _.Products, mce => mce.MapFrom(_ => _.Items));

        CreateMap<CartItem, CartItemResult>()
            .ForMember(_ => _.Cancelled, mce => mce.MapFrom(new CardItemCancelledResolver(), i => i.PurchaseStatus));
    }

    private class CardCancelledResolver : IMemberValueResolver<Cart, CartResult, PurchaseStatus, bool>
    {
        public bool Resolve(Cart source,CartResult destination, PurchaseStatus sourceMember, bool destMember, ResolutionContext context) =>
            source.PurchaseStatus is PurchaseStatus.Cancelled;
    }

    private class CardItemCancelledResolver : IMemberValueResolver<CartItem, CartItemResult, PurchaseStatus, bool>
    {
        public bool Resolve(CartItem source, CartItemResult destination, PurchaseStatus sourceMember, bool destMember, ResolutionContext context) =>
            source.PurchaseStatus is PurchaseStatus.Cancelled;
    }
}
