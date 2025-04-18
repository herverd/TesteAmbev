using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Serilog;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a products in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the product's title.
    /// Must not be null or empty.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the product's full price.
    /// Must be greater than zero.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the product's cover image.
    /// </summary>
    public string Image { get; private set; }

    /// <summary>
    /// Gets the stock quantity
    /// </summary>
    public int StockQuantity { get; private set; }

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public Rating Rating { get; private set; }

    /// <summary>
    /// Gets the product's category.
    /// </summary>
    public virtual Category Category { get; set; }

    /// <summary>
    /// Change product info.
    /// </summary>
    /// <param name="title">Title of product.</param>
    /// <param name="price">Price of product.</param>
    /// <param name="description">Description of product.</param>
    /// <param name="image">Image of product.</param>
    /// <param name="rating">Rating of product.</param>
    public void Change(
        string title,
        decimal price,
        string description,
        string image,
        Rating rating,
        Category category)
    {
        Title = title;
        Price = price;
        Description = description;
        Image = image;
        Rating = rating;
        Category = category;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Indicates if category name is the same of associated with the product.
    /// </summary>
    /// <param name="categoryName">Name of category.</param>
    /// <returns>True case product already this category name, false otherwise.</returns>
    public bool SameCategoryName(string categoryName) =>
        Category?.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase) ?? false;

    /// <summary>
    /// Set stock quantity directly.
    /// </summary>
    /// <param name="quantity">Quantity to change.</param>
    /// <exception cref="ArgumentOutOfRangeException">Occurs when try set negative value</exception>
    public void SetStockQuantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must not be negative value to the change quantity.");
        }

        StockQuantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Decrease the product quantity in storage.
    /// </summary>
    /// <param name="quantity">Quantity to reduce</param>
    /// <exception cref="ArgumentOutOfRangeException">Occurs when try set negative value</exception>
    public void DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive value to the decrease quantity.");
        }

        StockQuantity -= quantity;

        if (StockQuantity < 0)
        {
            Log.Error("Stock quantity must not be negative in product#{Id} '{Title}', current stock: {StockQuantity}, try to decrease {quantity}.");
            throw new DomainException($"Stock quantity must not be negative in product '{Title}'.");
        }

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Increase the product quantity in storage.
    /// </summary>
    /// <param name="quantity">Quantity to increase</param>
    /// <exception cref="ArgumentOutOfRangeException">Occurs when try set negative value</exception>
    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive value to the increase quantity.");
        }

        StockQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the user entity using the <see cref="ProductValidator"/> rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title length</list>
    /// <list type="bullet">Description length</list>
    /// <list type="bullet">Price amount</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
