using Patterns.Result;

namespace Patterns.Result.Demo;

static class Program
{
    static int Main()
    {
        var ok = LookupUser("ada");
        Print(ok);

        var missing = LookupUser("unknown");
        Print(missing);

        return ok.IsSuccess && missing.IsFailure ? 0 : 1;
    }

    /// <summary>
    /// Pretend lookup that returns success or failure instead of throwing.
    /// </summary>
    static Result<User> LookupUser(string id)
    {
        if (id == "ada")
        {
            return Result<User>.Success(new User("ada", "Ada Lovelace"));
        }

        return Result<User>.NotFound(
        [
            new Problem(nameof(id), $"No user with id '{id}'.", "user.not_found")
        ]);
    }

    static void Print(Result<User> result)
    {
        if (result.IsSuccess)
        {
            Console.WriteLine($"OK ({result.Status}): {result.Value.Name}");
            return;
        }

        Console.WriteLine($"FAIL ({result.Status}, http={result.StatusCode()}):");
        foreach (var problem in result.Problems)
        {
            Console.WriteLine($"  - {problem.Name}: {problem.Reason} [{problem.Code}]");
        }
    }
}

sealed record User(string Id, string Name);
