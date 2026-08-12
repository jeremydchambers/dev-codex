namespace Patterns.Result;

public enum ProblemSeverity
{
    Info,
    Warning,
    Error
}

public interface IProblem
{
    string Name { get; }
    string Reason { get; }
    string? Code { get; }
    ProblemSeverity Severity { get; }
}

public class Problem(string name, string reason, string? code = null, ProblemSeverity severity = ProblemSeverity.Error)
    : IProblem, IEquatable<Problem>
{
    public string Name { get; init; } = name;
    public string Reason { get; init; } = reason;
    public string? Code { get; set; } = code;
    public ProblemSeverity Severity { get; init; } = severity;

    public bool Equals(Problem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Name == other.Name
            && Reason == other.Reason
            && Code == other.Code
            && Severity == other.Severity;
    }
}
