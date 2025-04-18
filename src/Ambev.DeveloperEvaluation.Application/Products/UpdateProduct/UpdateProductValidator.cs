using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for product creation command.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <listheader>The validation includes checking:</listheader
    /// <list type="bullet">Id: Required, must not be emtpy</list>
    /// <list type="bullet">Title: Required, must be between 3 and 100 characters</list>
    /// <list type="bullet">Description: Required, must be between 3 and 300 characters</list>
    /// <list type="bullet">Price: Required, must be greater than zero</list>
    /// <list type="bullet">Image: Required, must be between 3 and 100 characters</list>
    /// <list type="bullet">Rating: Required, must be not null</list>
    /// <list type="bullet">Category name: Required, must be between 3 and 100 characters</list>
    /// </remarks>
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.Title).NotEmpty().Length(3, 100);
        RuleFor(product => product.Description).NotEmpty().Length(3, 300);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.CategoryName).NotEmpty().Length(3, 100);

        RuleFor(product => product.Image)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Product image must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product image cannot be longer than 100 characters.");

        RuleFor(product => product.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product quantity must be greater than or equal to 0.");

        RuleFor(product => product.Rating)
            .NotNull();

        When(p => p.Rating is not null, () =>
        {
            RuleFor(p => p.Rating.Rate)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product rating must be greater than or equal to 0.");

            RuleFor(p => p.Rating.Count)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product rating count must be greater than or equal to 0.");
        });
    }
}
