namespace Patterns.Result;

public static class ResultStatusExtensions
{
    /// <summary>
    /// Maps a <see cref="ResultStatus"/> to a conventional HTTP status code.
    /// Useful when bridging Result-based flow to an HTTP response; the Result
    /// type itself does not depend on ASP.NET.
    /// </summary>
    public static int ToStatusCode(this ResultStatus status)
    {
        return status switch
        {
            ResultStatus.DataFailure => 500,
            ResultStatus.MappingFailure => 500,
            ResultStatus.BadRequest => 400,
            ResultStatus.Conflict => 409,
            ResultStatus.Created => 201,
            ResultStatus.Success => 200,
            ResultStatus.NotFound => 404,
            ResultStatus.Timeout => 408,
            _ => 501
        };
    }

    public static bool IsNotFound(this ResultStatus resultStatus)
    {
        return resultStatus == ResultStatus.NotFound;
    }
}
