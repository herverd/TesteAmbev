using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelCart;

/// <summary>
/// Validator for <see cref="CancelCartRequest"/>.
/// </summary>
public class CancelCartRequestValidator : AbstractValidator<CancelCartRequest>
{
    /// <summary>
    /// Initializes validation rules for <see cref="CancelCartRequest"/>.
    /// </summary>
    public CancelCartRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}
