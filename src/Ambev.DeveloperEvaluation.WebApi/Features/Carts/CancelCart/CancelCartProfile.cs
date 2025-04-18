using Ambev.DeveloperEvaluation.Application.Carts.CancelCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelCart;

/// <summary>
/// Profile for mapping between Application and API DeleteCart responses.
/// </summary>
public class CancelCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteCart feature.
    /// </summary>
    public CancelCartProfile()
    {
        CreateMap<CancelCartRequest, CancelCartCommand>();
    }
}
