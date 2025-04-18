using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Type { get; private set; } = ApiResponseErrorType.None.ToString();
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];

    public static ApiResponse CreateAsValidationError(string error, string detail) =>
        CreateError(ApiResponseErrorType.ValidationError, error, detail);

    public static ApiResponse CreateAsValidationError(IEnumerable<ValidationErrorDetail> errors) =>
        CreateError(ApiResponseErrorType.ValidationError, errors);

    public static ApiResponse CreateAsNotFound(string error, string detail) =>
        CreateError(ApiResponseErrorType.ResourceNotFound, error, detail);

    public static ApiResponse CreateAsNotFound(string message = "Resource not found") =>
        CreateError(ApiResponseErrorType.ResourceNotFound, message, string.Empty);

    public static ApiResponse CreateError(ApiResponseErrorType type, string error, string detail) =>
        CreateError(type,
        [
            new()
            {
                Error = error,
                Detail = detail,
            }
        ]);

    public static ApiResponse CreateError(ApiResponseErrorType type, IEnumerable<ValidationErrorDetail> errors) => new()
    {
        Success = false,
        Type = type.ToString(),
        Message = errors.FirstOrDefault()?.Error ?? "Validation Failed",
        Errors = errors,
    };
}
