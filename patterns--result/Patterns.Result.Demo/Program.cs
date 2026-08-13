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

    static Result<User> LookupUser(string id)
    {
        if (id == "ada")
        {
            return Result<User>.Success(new User("ada", "Ada Lovelace"));
        }

        return Result<User>.Failure($"No user with id '{id}'.");
    }

    static void Print(Result<User> result)
    {
        Console.WriteLine(result.Map(
            user => $"OK: {user.Name}",
            error => $"FAIL: {error}"));
    }
}

sealed record User(string Id, string Name);
