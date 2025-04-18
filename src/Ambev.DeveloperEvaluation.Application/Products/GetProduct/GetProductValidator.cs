using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Validator for <see cref="GetProductCommand"/>.
/// </summary>
public class GetProductValidator : AbstractValidator<GetProductCommand>
{
    /// <summary>
    /// Initializes validation rules for <see cref="GetProductCommand"/>.
    /// </summary>
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
