namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Define o contrato para representação de um usuário no sistema.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Obtém o identificador único do usuário.
        /// </summary>
        /// <returns>O ID do usuário como uma string.</returns>
        Guid Id { get; }

        /// <summary>
        /// Obtém o nome de usuário.
        /// </summary>
        /// <returns>O nome de usuário.</returns>
        string Username { get; }

        /// <summary>
        /// Gets e-mail from user.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Obtém o papel/função do usuário no sistema.
        /// </summary>
        /// <returns>O papel do usuário como uma string.</returns>
        string Role { get; }
    }
}
