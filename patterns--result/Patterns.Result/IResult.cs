namespace Patterns.Result;

/// <summary>
/// Defines the interface for a result.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets a value indicating whether the result is successful.
    /// </summary>
    bool IsSuccess { get; }

    bool IsFailure { get; }

    /// <summary>
    /// Gets the status of the result.
    /// </summary>
    ResultStatus Status { get; }

    /// <summary>
    /// Gets the list of problems associated with the result.
    /// </summary>
    List<IProblem> Problems { get; }
}

/// <summary>
/// Defines the interface for a result that contains a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the value contained in the result.</typeparam>
public interface IResult<out T> : IResult
{
    /// <summary>
    /// Gets the value of the result.
    /// </summary>
    T? Value { get; }
}
