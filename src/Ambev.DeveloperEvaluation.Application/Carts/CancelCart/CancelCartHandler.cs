﻿using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CancelCart;

/// <summary>
/// Handler for processing <see cref="CancelCartCommand"/> requests.
/// </summary>
public class CancelCartHandler : IRequestHandler<CancelCartCommand, CancelCartResponse>
{
    private readonly ICurrentUserAccessor _currentUserAccessor;
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventNotification _eventNotifier;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of <see cref="CancelCartHandler"/>.
    /// </summary>
    /// <param name="currentUserAccessor">Accessor to current user of system</param>
    /// <param name="cartRepository">The cart repository</param>
    /// <param name="userRepository">The user repository</param>
    /// <param name="eventNotifier">Notifier of events</param>
    /// <param name="unitOfWork">Unit of work</param>
    public CancelCartHandler(
        ICurrentUserAccessor currentUserAccessor,
        ICartRepository cartRepository,
        IUserRepository userRepository,
        IEventNotification eventNotifier,
        IUnitOfWork unitOfWork)
    {
        _currentUserAccessor = currentUserAccessor;
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _eventNotifier = eventNotifier;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the <see cref="CancelCartCommand"/> request.
    /// </summary>
    /// <param name="request">The DeleteProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<CancelCartResponse> Handle(CancelCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CancelCartValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var currentUserInfo = _currentUserAccessor.GetCurrentUser();
        var currentUser = await _userRepository.GetByIdAsync(currentUserInfo.Id, cancellationToken);
        if (currentUser is null || currentUser.Status is not UserStatus.Active)
            throw new NotFoundDomainException(BusinessRuleMessages.UserNotFound(currentUserInfo.Id));

        var cart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken);
        if (cart is null)
            throw new NotFoundDomainException(BusinessRuleMessages.CardNotFound(command.Id));

        if (!cart.CanBeCancel)
            throw new NotFoundDomainException(BusinessRuleMessages.CardCannotBeCancel(command.Id));

        cart.Cancel(currentUser);

        await _unitOfWork.ApplyChangesAsync(cancellationToken);

        await _eventNotifier.NotifyAsync(SaleCancelledEvent.CreateFrom(cart));

        return new CancelCartResponse { Success = true };
    }
}
