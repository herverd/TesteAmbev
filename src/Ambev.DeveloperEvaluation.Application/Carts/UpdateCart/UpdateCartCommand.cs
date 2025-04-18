using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;

/// <summary>
/// Command for updating a existing cart.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a product, 
/// including productname, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CartResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="UpdateCartCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateCartCommand : IRequest<CartResult>
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets a customer who bought a cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets a date of sale.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets a branch where the sale was made
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Gets products in the cart.
    /// </summary>
    public ICollection<UpdateCartItem> Products { get; set; } = [];

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
