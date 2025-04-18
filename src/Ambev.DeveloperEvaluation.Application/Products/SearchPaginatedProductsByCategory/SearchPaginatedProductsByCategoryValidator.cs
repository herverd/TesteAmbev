using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.SearchPaginatedProductsByCategory;

/// <summary>
/// Validator for <see cref="SearchPaginatedProductsByCategoryCommand"/>.
/// </summary>
public class SearchPaginatedProductsByCategoryValidator : AbstractValidator<SearchPaginatedProductsByCategoryCommand>
{
    /// <summary>
    /// Initializes validation rules for <see cref="SearchPaginatedProductsByCategoryCommand"/>.
    /// </summary>
    public SearchPaginatedProductsByCategoryValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .WithMessage("Category name is required to search.");
    }
}
