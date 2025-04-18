using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected IActionResult Ok<T>(T data, string? message = default) =>
        base.Ok(new ApiResponseWithData<T> { Data = data, Success = true, Message = message ?? string.Empty });

    protected IActionResult Ok(string message) =>
        base.Ok(new ApiResponseWithData<object> { Data = null, Success = true, Message = message });

    protected IActionResult Created<T>(string routeName, object routeValues, T data, string? message = default) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true, Message = message ?? string.Empty });

    protected IActionResult BadRequest(List<ValidationFailure> errors) =>
        base.BadRequest(ApiResponse.CreateAsValidationError(errors.Select(ValidationErrorDetail.ConvertFrom)));

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(ApiResponse.CreateAsNotFound(message));

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
        base.Ok(new PaginatedResponse<T>
        {
            Data = pagedList,
            CurrentPage = pagedList.CurrentPage,
            TotalPages = pagedList.TotalPages,
            TotalItems = pagedList.TotalCount,
            Success = true
        });
}
