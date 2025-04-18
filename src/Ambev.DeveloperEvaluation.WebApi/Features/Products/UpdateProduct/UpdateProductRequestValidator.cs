﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for <see cref="UpdateProductRequest"/> that defines validation rules for product creation.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductRequestValidator"/> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Id: Required, must be empty</list>
    /// <list type="bullet">Title: Required, must be between 3 and 100 characters</list>
    /// <list type="bullet">Description: Required, must be between 3 and 300 characters</list>
    /// <list type="bullet">Price: Required, must be greater than zero</list>
    /// <list type="bullet">Image: Required, must be between 3 and 100 characters</list>
    /// <list type="bullet">Rating: Required, must be not null</list>
    /// <list type="bullet">Category: Required, must be between 3 and 100 characters</list>
    /// </remarks>
    public UpdateProductRequestValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.Title).NotEmpty().Length(3, 100);
        RuleFor(product => product.Description).NotEmpty().Length(3, 300);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Category).NotEmpty().Length(3, 100);

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
