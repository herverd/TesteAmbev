using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a category of product in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    public Category()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the category's name.
    /// Must not be null or empty.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets the date and time when the category was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the date and time of the last update to the category's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Gets relationship of products.
    /// </summary>
    public virtual ICollection<Product> Products { get; private set; } = [];

    /// <summary>
    /// Change category info.
    /// </summary>
    /// <param name="name">Name of product.</param>
    public void Change(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the user entity using the <see cref="CategoryValidator"/> rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Name length</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new CategoryValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
