using System.Diagnostics.CodeAnalysis;

namespace Patterns.Result;

public class Result : IResult, IEquatable<Result>
{
    public virtual bool IsSuccess => Status == ResultStatus.Success || Status == ResultStatus.Created;

    public virtual bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the status of the result.
    /// </summary>
    public ResultStatus Status { get; }

    /// <summary>
    /// Gets the list of problems associated with the result.
    /// </summary>
    public List<IProblem> Problems { get; } = new List<IProblem>();

    /// <summary>
    /// Initialize a new instance using an existing one
    /// </summary>
    /// <param name="result"></param>
    protected Result(IResult result) => (Status, Problems) = (result.Status, result.Problems);

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified status.
    /// </summary>
    /// <param name="status">The status of the result.</param>
    protected Result(ResultStatus status) => Status = status;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified status and problems.
    /// </summary>
    /// <param name="status">The status of the result.</param>
    /// <param name="problems">The problems associated with the result.</param>
    protected Result(ResultStatus status, IEnumerable<IProblem> problems) => (Status, Problems) = (status, problems.ToList());

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Success() => new(ResultStatus.Success);

    /// <summary>
    /// Creates a BadRequest result.
    /// </summary>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure() => new(ResultStatus.BadRequest);

    /// <summary>
    /// Creates a BadRequest result with the specified problems.
    /// </summary>
    /// <param name="problems">The problems associated with the result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result Failure(List<IProblem> problems) => new(ResultStatus.BadRequest, problems);

    public static Result Failure(Exception ex) => new(ResultStatus.BadRequest, ex.ToProblems());

    public static Result Failure(Result result) => new(result.Status, result.Problems);

    /// <summary>
    /// Creates a BadRequest result.
    /// </summary>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result BadRequest() => new(ResultStatus.BadRequest);

    public static Result DataFailure() => new(ResultStatus.DataFailure);
    public static Result DataFailure(List<IProblem> problems) => new(ResultStatus.DataFailure, problems);

    public static Result MappingFailure() => new(ResultStatus.MappingFailure);
    public static Result MappingFailure(List<IProblem> problems) => new(ResultStatus.MappingFailure, problems);

    public static Result InternalFailure() => new(ResultStatus.InternalFailure);
    public static Result InternalFailure(List<IProblem> problems) => new(ResultStatus.InternalFailure, problems);

    public static Result Timeout() => new(ResultStatus.Timeout);
    public static Result Timeout(List<IProblem> problems) => new(ResultStatus.Timeout, problems);

    public static Result Conflict() => new(ResultStatus.Conflict);

    /// <summary>
    /// Creates a NotFound result.
    /// </summary>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    /// Creates a NotFound result with the specified problems.
    /// </summary>
    /// <param name="problems">The problems associated with the result.</param>
    /// <returns>A failed <see cref="Result"/>.</returns>
    public static Result NotFound(List<IProblem> problems) => new(ResultStatus.NotFound, problems);

    /// <summary>
    /// Maps the result to a value based on success or failure.
    /// </summary>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onProblem">The function to execute if the result has problems.</param>
    /// <returns>The mapped result value.</returns>
    public TResult Map<TResult>(Func<TResult> onSuccess, Func<List<IProblem>, TResult> onProblem)
    {
        return IsSuccess ? onSuccess() : onProblem(Problems);
    }

    public bool Equals(Result? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (Status != other.Status)
        {
            return false;
        }

        if (Problems.Count != other.Problems.Count)
        {
            return false;
        }

        for (int i = 0; i < Problems.Count; i++)
        {
            if (!Problems[i].Equals(other.Problems[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is Result other)
        {
            if (Status != other.Status)
            {
                return false;
            }

            if (Problems.Count != other.Problems.Count)
            {
                return false;
            }

            for (int i = 0; i < Problems.Count; i++)
            {
                if (!Problems[i].Equals(other.Problems[i]))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

/// <summary>
/// Represents a result with a status and associated problems.
/// </summary>
/// <typeparam name="T">The type of the value associated with a successful result.</typeparam>
public class Result<T> : Result, IResult<T>
{
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess => base.IsSuccess;

    [MemberNotNullWhen(false, nameof(Value))]
    public override bool IsFailure => base.IsFailure;

    /// <summary>
    /// Gets the value associated with a successful result.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value associated with a successful result.</param>
    protected Result(T? value) : base(ResultStatus.Success) => Value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value and status.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <param name="status">The status of the result.</param>
    protected Result(T? value, ResultStatus status) : base(status) => Value = value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified status and problems.
    /// </summary>
    /// <param name="status">The status of the result.</param>
    /// <param name="problems">The problems associated with the result.</param>
    protected Result(ResultStatus status, IEnumerable<IProblem> problems) : base(status, problems) => Value = default;

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value associated with the result.</param>
    /// <returns>A successful <see cref="Result{T}"/>.</returns>
    public static Result<T> Success(T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new(value);
    }

    public static Result<T> Created(T value) => new(value, ResultStatus.Created);

    /// <summary>
    /// Creates a BadRequest result with the specified problems.
    /// </summary>
    /// <param name="problems">The problems associated with the result.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static Result<T> Failure(IEnumerable<IProblem> problems) => new(ResultStatus.BadRequest, problems);

    public static Result<T> Failure(IProblem problem) => new(ResultStatus.BadRequest, [problem]);

    /// <summary>
    /// Creates a BadRequest result with the specified problems.
    /// </summary>
    /// <param name="ex">The exception to convert into problems.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static new Result<T> Failure(Exception ex) => new(ResultStatus.BadRequest, ex.ToProblems());

    /// <summary>
    /// Creates a failed result that copies status and problems from another result.
    /// </summary>
    /// <param name="result">The result to copy from.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static new Result<T> Failure(Result result) => new(result.Status, result.Problems);

    /// <summary>
    /// Creates a NotFound result with the specified problems.
    /// </summary>
    /// <param name="problems">The problems associated with the result.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static Result<T> NotFound(IEnumerable<IProblem> problems) => new(ResultStatus.NotFound, problems);

    public static new Result<T> NotFound() => new(ResultStatus.NotFound, []);

    /// <summary>
    /// Creates a BadRequest result with the specified problems.
    /// </summary>
    /// <param name="problems">The problems associated with the result.</param>
    /// <returns>A failed <see cref="Result{T}"/>.</returns>
    public static Result<T> BadRequest(IEnumerable<IProblem> problems) => new(ResultStatus.BadRequest, problems);

    public static new Result<T> BadRequest() => new(ResultStatus.BadRequest, []);

    public static Result<T> InternalFailure(Result result) => new(result.Status, result.Problems);
    public static Result<T> InternalFailure(IEnumerable<IProblem> problems) => new(ResultStatus.InternalFailure, problems);
    public static new Result<T> InternalFailure() => new(ResultStatus.InternalFailure, []);

    public static Result<T> Timeout(Result result) => new(result.Status, result.Problems);
    public static Result<T> Timeout(IEnumerable<IProblem> problems) => new(ResultStatus.Timeout, problems);
    public static new Result<T> Timeout() => new(ResultStatus.Timeout, []);

    public static Result<T> DataFailure(Result result) => new(result.Status, result.Problems);
    public static Result<T> DataFailure(IEnumerable<IProblem> problems) => new(ResultStatus.DataFailure, problems);
    public static new Result<T> DataFailure() => new(ResultStatus.DataFailure, []);

    public static Result<T> MappingFailure(Result result) => new(result.Status, result.Problems);
    public static Result<T> MappingFailure(IEnumerable<IProblem> problems) => new(ResultStatus.MappingFailure, problems);
    public static new Result<T> MappingFailure() => new(ResultStatus.MappingFailure, []);

    public static Result<T> Conflict(IEnumerable<IProblem> problems) => new(ResultStatus.Conflict, problems);
    public static new Result<T> Conflict() => new(ResultStatus.Conflict, []);

    /// <summary>
    /// Implicitly converts a value to a successful result.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<T>(T value) => new(value);

    /// <summary>
    /// Maps the result to a value based on success or failure.
    /// </summary>
    /// <typeparam name="TResult">The type of the result value.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is successful.</param>
    /// <param name="onProblem">The function to execute if the result has problems.</param>
    /// <returns>The mapped result value.</returns>
    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<List<IProblem>, TResult> onProblem)
    {
        return IsSuccess ? onSuccess(Value!) : onProblem(Problems);
    }
}
