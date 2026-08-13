using System.Diagnostics.CodeAnalysis;

namespace Patterns.Result;

public sealed class Result
{
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;

    public string? Error { get; }

    private Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(string error)
    {
        if (string.IsNullOrWhiteSpace(error))
        {
            throw new ArgumentException("Failure requires an error message.", nameof(error));
        }

        return new(false, error);
    }

    public TResult Map<TResult>(Func<TResult> onSuccess, Func<string, TResult> onError)
        => IsSuccess ? onSuccess() : onError(Error);
}
