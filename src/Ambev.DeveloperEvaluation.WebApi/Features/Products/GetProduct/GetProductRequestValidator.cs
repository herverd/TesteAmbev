using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Validator for <see cref="GetProductRequest"/>.
/// </summary>
public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    /// <summary>
    /// Initializes validation rules for <see cref="GetProductRequest"/>.
    /// </summary>
    public GetProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
