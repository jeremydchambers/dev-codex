using System.Diagnostics.CodeAnalysis;

namespace Patterns.Result;

public sealed class Result<T>
{
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(false, nameof(Value))]
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public string? Error { get; }

    private Result(T? value, string? error, bool isSuccess)
    {
        Value = value;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return new(value, null, true);
    }

    public static Result<T> Failure(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
        {
            throw new ArgumentException("Failure requires an error message.", nameof(error));
        }

        return new(default, error, false);
    }

    public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onError)
        => IsSuccess ? onSuccess(Value) : onError(Error);
}
