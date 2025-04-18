using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a product, 
/// including name and price. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateProductValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<ProductResult>
{
    /// <summary>
    /// Gets or sets the name of the product to be created.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets the price for the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the product's cover image.
    /// </summary>
    public string Image { get; set; } = default!;

    /// <summary>
    /// Gets the stock quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public Rating Rating { get; set; } = default!;

    /// <summary>
    /// Gets the product's category name.
    /// </summary>
    public string CategoryName { get; private set; } = default!;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
