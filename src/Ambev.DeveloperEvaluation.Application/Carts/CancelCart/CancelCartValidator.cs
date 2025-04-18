using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CancelCart;

/// <summary>
/// Validator for <see cref="CancelCartCommand"/>.
/// </summary>
public class CancelCartValidator : AbstractValidator<CancelCartCommand>
{
    /// <summary>
    /// Initializes validation rules for <see cref="CancelCartCommand"/>.
    /// </summary>
    public CancelCartValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required.");
    }
}
