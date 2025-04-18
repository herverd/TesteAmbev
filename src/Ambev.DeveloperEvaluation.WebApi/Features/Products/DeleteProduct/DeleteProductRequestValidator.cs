using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

/// <summary>
/// Validator for <see cref="DeleteProductRequest"/>.
/// </summary>
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    /// <summary>
    /// Initializes validation rules for <see cref="DeleteProductRequest"/>.
    /// </summary>
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
