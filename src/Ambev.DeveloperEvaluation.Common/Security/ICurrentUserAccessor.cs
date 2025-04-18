namespace Ambev.DeveloperEvaluation.Common.Security;

/// <summary>
/// Defines a way to provide access to the current user info.
/// </summary>
public interface ICurrentUserAccessor
{
    /// <summary>
    /// Retrieves current system´s user.
    /// </summary>
    /// <returns>User of the system.</returns>
    IUser GetCurrentUser();
}