using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Defines way to convention response api error.
/// </summary>
internal sealed class ApiErrorsConventionHandler
{
    /// <summary>
    /// Occurs when not found resource by route.
    /// </summary>
    private static readonly ApiResponse ResourceNotFoundErrorResponse = ApiResponse.CreateError(
        ApiResponseErrorType.ResourceNotFound,
        "Uri not found",
        "Endpoint does not exist");

    /// <summary>
    /// Occurs when authentication failed.
    /// </summary>
    private static readonly ApiResponse AuthenticationErrorResponse = ApiResponse.CreateError(
        ApiResponseErrorType.AuthenticationError,
        "Invalid authentication token",
        "The provided authentication token has expired or is invalid");

    /// <summary>
    /// Handle bad request when is invalid mvc model validation.
    /// </summary>
    /// <param name="context">Action context</param>
    /// <returns>Action result</returns>
    public static IActionResult HandleBadRequestOnInvalidModelValidation(ActionContext context)
    {
        var error = context.ModelState.FirstOrDefault(_ => _.Key.StartsWith('$'), context.ModelState.FirstOrDefault());
        var problemDetails = ApiResponse.CreateAsValidationError(
            "Invalid input data",
            error.Value?.Errors.Select(e => e.ErrorMessage).FirstOrDefault() ?? "Error");

        return new BadRequestObjectResult(problemDetails);
    }

    /// <summary>
    /// Handle error by status code.
    /// </summary>
    /// <param name="context">Status code context</param>
    /// <returns>Asynchronous task.</returns>
    public static Task HandleByStatusCode(StatusCodeContext context)
    {
        var errorResponse = context.HttpContext.Response.StatusCode switch
        {
            StatusCodes.Status401Unauthorized => AuthenticationErrorResponse,
            StatusCodes.Status404NotFound => ResourceNotFoundErrorResponse,
            _ => null,
        };

        return context.HttpContext.Response.WriteAsJsonAsync(errorResponse);
    }
}