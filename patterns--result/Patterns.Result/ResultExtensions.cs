namespace Patterns.Result;

public static class ResultExtensions
{
    public static bool IsFailure(this Result result) => !result.IsSuccess;

    public static int StatusCode(this Result result) => result.Status.ToStatusCode();
}
