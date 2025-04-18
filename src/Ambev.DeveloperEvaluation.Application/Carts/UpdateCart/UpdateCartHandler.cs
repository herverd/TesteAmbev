using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Handler for processing <see cref="UpdateCartCommand"/> requests.
/// </summary>
public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, CartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly SaleDiscountService _saleDiscountService;
    private readonly SaleLimitReachedSpecification _saleLimitReachedSpecification;
    private readonly IEventNotification _eventNotifier;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateCartHandler"/>.
    /// </summary>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="userRepository">The user repository</param>
    /// <param name="productRepository">The product repository</param>
    /// <param name="currentUserAccessor">Accessor to current user of system</param>
    /// <param name="saleDiscountService">Service to apply discounts</param>
    /// <param name="saleLimitReachedSpecification">Specification to validate if sale limit was reached</param>
    /// <param name="eventNotifier">Notifier of events</param>
    /// <param name="unitOfWork">Unit of work.</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateCartHandler(
        ICartRepository cartRepository,
        IUserRepository userRepository,
        IProductRepository productRepository,
        ICurrentUserAccessor currentUserAccessor,
        SaleDiscountService saleDiscountService,
        SaleLimitReachedSpecification saleLimitReachedSpecification,
        IEventNotification eventNotifier,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _currentUserAccessor = currentUserAccessor;
        _saleDiscountService = saleDiscountService;
        _saleLimitReachedSpecification = saleLimitReachedSpecification;
        _eventNotifier = eventNotifier;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="UpdateCartHandler"/> request.
    /// </summary>
    /// <param name="command">The UpdateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    public async Task<CartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var currentUserInfo = _currentUserAccessor.GetCurrentUser();
        var currentUser = await _userRepository.GetByIdAsync(currentUserInfo.Id, cancellationToken);
        if (currentUser is null || currentUser.Status is not UserStatus.Active)
        {
            throw new NotFoundDomainException(BusinessRuleMessages.UserNotFound(currentUserInfo.Id));
        }

        var customerUser = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (customerUser is null || customerUser.Status is not UserStatus.Active)
        {
            throw new NotFoundDomainException(BusinessRuleMessages.UserNotFound(command.UserId));
        }

        var cart = await _cartRepository.GetByIdWithActiveItemsAsync(command.Id, cancellationToken);
        if (cart is null)
        {
            throw new NotFoundDomainException(BusinessRuleMessages.CardNotFound(command.Id));
        }

        if (!cart.CanBeEdited)
            throw new NotFoundDomainException(BusinessRuleMessages.CardCannotBeEdited(command.Id));

        var cartItems = await CreateItemsAsync(command, currentUser, cancellationToken);

        ChangeCart(cart, command, customerUser, currentUser, cartItems);

        if (_saleLimitReachedSpecification.IsSatisfiedBy(cart))
        {
            throw new DomainException(BusinessRuleMessages.ProductSaleLimitReached(_saleLimitReachedSpecification.MaximumItemsPerProduct).Detail);
        }

        _saleDiscountService.ApplyDiscounts(cart);

        await _unitOfWork.ApplyChangesAsync(cancellationToken);

        await _eventNotifier.NotifyAsync(SaleModifiedEvent.CreateFrom(cart));

        if (cart.HasAnyDeletedItem)
        {
            await _eventNotifier.NotifyAsync(ItemCancelledEvent.CreateFrom(cart));
        }

        return _mapper.Map<CartResult>(cart);
    }

    private static void ChangeCart(
        Cart cart,
        UpdateCartCommand command,
        User customerUser,
        User currentUser,
        IEnumerable<CartItem> cartItems)
    {
        cart.Change(customerUser, command.Date, command.Branch);

        var itemsToRemove = cart.Items
            .Where(i => !cartItems.Any(ci => ci.ProductId == i.ProductId))
            .ToArray();
        cart.DeleteItems(currentUser, itemsToRemove);

        var existingItems = cartItems
            .Where(i => cart.Items.Any(ci => ci.ProductId == i.ProductId))
            .ToArray();
        cart.ChangeQuantities(existingItems);

        var newItems = cartItems
            .Where(i => !cart.Items.Any(ci => ci.ProductId == i.ProductId))
            .ToArray();
        cart.AddItems(newItems);
    }

    private async Task<IEnumerable<CartItem>> CreateItemsAsync(
        UpdateCartCommand command,
        User loggedUser,
        CancellationToken cancellationToken)
    {
        command.Products = command.Products
            .GroupBy(p => p.ProductId)
            .Select(g => new UpdateCartItem
            {
                ProductId = g.Key,
                Quantity = g.Sum(_ => _.Quantity),
            })
            .ToArray();

        var productIds = command.Products.Select(p => p.ProductId).ToArray();
        ICollection<Product> products = await _productRepository.ListByIdsAsync(productIds, cancellationToken);

        if (products.Count != productIds.Length)
        {
            var missingProductIds = productIds
                .Where(id => !products.Any(p => p.Id == id));

            throw new NotFoundDomainException(BusinessRuleMessages.ProductsNotFound(missingProductIds));
        }

        return
            from p in products
            join c in command.Products on p.Id equals c.ProductId
            select CartItem.CreateForProduct(p, c.Quantity, loggedUser);
    }
}
