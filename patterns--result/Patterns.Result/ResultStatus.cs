namespace Patterns.Result;

/// <summary>
/// Represents the status of a result.
/// </summary>
public enum ResultStatus
{
    Success,
    Timeout,
    BadRequest,
    DataFailure,
    MappingFailure,
    InternalFailure,
    NotFound,
    Conflict,
    Created
}
