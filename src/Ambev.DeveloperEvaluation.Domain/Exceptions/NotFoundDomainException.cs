namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class NotFoundDomainException : DomainException
{
    public NotFoundDomainException(string message) : base(message)
    {
        Error = message;
        Detail = message;
    }

    public NotFoundDomainException(string error, string detail) : base(error)
    {
        Error = error;
        Detail = detail;
    }

    public NotFoundDomainException(BusinessRuleMessages.RuleMessage message) : base(message.Error)
    {
        Error = message.Error;
        Detail = message.Detail;
    }

    public string Error { get; init; }
    public string Detail { get; init; }
}