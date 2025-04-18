namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Defines types response error.
/// </summary>
public enum ApiResponseErrorType
{
    /// <summary>
    /// No erros.
    /// </summary>
    None,

    /// <summary>
    /// Resource not found.
    /// </summary>
    ResourceNotFound,

    /// <summary>
    /// Authentication error.
    /// </summary>
    AuthenticationError,

    /// <summary>
    /// Validation error.
    /// </summary>
    ValidationError,
}