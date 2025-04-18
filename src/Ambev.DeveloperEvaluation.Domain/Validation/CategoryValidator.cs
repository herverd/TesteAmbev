using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

internal class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Category name cannot be longer than 100 characters.");
    }
}
