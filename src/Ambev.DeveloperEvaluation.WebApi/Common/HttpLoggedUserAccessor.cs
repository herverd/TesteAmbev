using Ambev.DeveloperEvaluation.Common.Security;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Represents way to access current logged user from http context.
/// </summary>
/// <remarks>
/// Register as scoped context because it´s cached parsed user from claims.
/// </remarks>
internal class HttpLoggedUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;
    private LoggedUserClaims? _currentLoggedUser = null;

    public HttpLoggedUserAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public IUser GetCurrentUser()
    {
        if (_currentLoggedUser is not null)
        {
            return _currentLoggedUser;
        }

        var loggedUser = _contextAccessor.HttpContext?.User ?? throw new InvalidOperationException("User must have be logged in the system.");

        var userId = Guid.Parse(loggedUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());
        var username = loggedUser.FindFirst(ClaimTypes.Name)?.Value ?? throw new NullReferenceException();
        var userEmail = loggedUser.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();
        var userRoleName = loggedUser.FindFirst(ClaimTypes.Role)?.Value ?? throw new NullReferenceException();

        return _currentLoggedUser ??= new()
        {
            Id = userId,
            Username = username,
            Email = userEmail,
            Role = userRoleName,
        };
    }

    private record LoggedUserClaims : IUser
    {
        public required Guid Id { get; init; }

        public required string Username { get; init; }

        public required string Email { get; init; }

        public required string Role { get; init; }
    }
}