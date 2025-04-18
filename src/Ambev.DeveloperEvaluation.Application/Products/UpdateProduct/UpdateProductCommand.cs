using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Command for updating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a product, 
/// including productname, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="ProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateProductCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateProductCommand : IRequest<ProductResult>
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product's name.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// The product's price.
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
        var validator = new UpdateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
