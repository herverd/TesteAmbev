namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

/// <summary>
/// Provides a business rule messages to domain.
/// </summary>
public sealed class BusinessRuleMessages
{
    public static readonly Func<Guid, RuleMessage> UserNotFound = id => new("User not found", $"The user with ID {id} does not exist in our database");
    public static readonly Func<string, RuleMessage> UserEmailExists = email => new("User e-mail exists", $"User with email {email} already exists");

    public static readonly Func<Guid, RuleMessage> CardNotFound = id => new("Cart not found", $"The cart with ID {id} does not exist in our database");
    public static readonly Func<Guid, RuleMessage> CardCannotBeCancel = id => new("Cart can´t cancel", $"Cart #{id} cannot be cancel");
    public static readonly Func<Guid, RuleMessage> CardCannotBeEdited = id => new("Cart can´t edit", $"Cart #{id} cannot be edit");

    public static readonly Func<Guid, RuleMessage> ProductNotFound = id => new("Product not found", $"The product with ID {id} does not exist in our database");
    public static readonly Func<IEnumerable<Guid>, RuleMessage> ProductsNotFound = ids => new("Products are not found", $"The products with IDs {string.Join(", ", ids.Select(_ => _.ToString()))} does not exists in our database");
    public static readonly Func<int, RuleMessage> ProductSaleLimitReached = total => new("Product sale limit reached", $"Cannot sell more than {total} items per product");
    public static readonly Func<string, RuleMessage> ProductTitleExists = title => new("Product title exists", $"Product with title {title} already exists");

    public sealed record RuleMessage(string Error, string Detail);
}