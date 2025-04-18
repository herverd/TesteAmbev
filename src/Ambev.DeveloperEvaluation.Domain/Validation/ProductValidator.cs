using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

internal class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product name cannot be longer than 100 characters.");

        RuleFor(product => product.Description)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Product description must be at least 3 characters long.")
            .MaximumLength(300).WithMessage("Product description cannot be longer than 300 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Product image must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product image cannot be longer than 100 characters.");

        RuleFor(product => product.Rating)
            .NotNull();

        When(p => p.Rating is not null, () =>
        {
            RuleFor(p => p.Rating.Rate)
                .GreaterThan(0).WithMessage("Product rating must be greater than 0.");

            RuleFor(p => p.Rating.Count)
                .GreaterThan(0).WithMessage("Product rating count must be greater than 0.");
        });

        RuleFor(product => product.Category)
            .NotNull();
    }
}
