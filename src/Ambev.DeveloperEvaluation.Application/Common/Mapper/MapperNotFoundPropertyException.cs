namespace Ambev.DeveloperEvaluation.Application.Common.Mapper;

/// <summary>
/// Exception to be raised when a property is not found during property mapping between layers.
/// </summary>
public class MapperNotFoundPropertyException : Exception
{
    public MapperNotFoundPropertyException(string message) : base(message)
    {
    }
}
