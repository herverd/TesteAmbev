namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Defines a status of purcharse.
/// </summary>
public enum PurchaseStatus
{
    /// <summary>
    /// Initial status.
    /// </summary>
    Created,

    /// <summary>
    /// When a order was cancelled.
    /// </summary>
    Cancelled,

    /// <summary>
    /// When a order was deleted.
    /// </summary>
    Deleted,
}
