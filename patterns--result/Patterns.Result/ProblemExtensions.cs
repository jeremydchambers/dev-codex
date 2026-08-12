namespace Patterns.Result;

internal static class ProblemExtensions
{
    public static IProblem ToProblem(this Exception ex)
        => new Problem(ex.GetType().Name, ex.Message, default, ProblemSeverity.Error);

    public static List<IProblem> ToProblems(this Exception ex)
        => [ex.ToProblem()];
}
