using Ambev.DeveloperEvaluation.Application.Common.Mapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.PaginateCarts;

/// <summary>
/// Handler for processing <see cref="PaginateCartsCommand"/> requests.
/// </summary>
public class PaginateCartsHandler : IRequestHandler<PaginateCartsCommand, PaginateCartsResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="PaginateCartsHandler"/>.
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public PaginateCartsHandler(
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the PaginateCartsCommand request
    /// </summary>
    /// <param name="request">The PaginateCarts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    public async Task<PaginateCartsResult> Handle(PaginateCartsCommand request, CancellationToken cancellationToken)
    {
        // note: maps fields from the DTO result to the domain, with names mapped accordingly using AutoMapper.
        var paging = _mapper.MapFrom(request, [
            new(typeof(Cart), typeof(CartResult)),
            new(typeof(CartItem), typeof(CartItemResult))]);

        var paginationResult = await _cartRepository.PaginateAsync(paging, cancellationToken);
        return _mapper.Map<PaginateCartsResult>(paginationResult);
    }
}
