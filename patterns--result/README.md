# Result (control-flow utility)

## What it demonstrates

A small C# `Result` / `Result<T>` kit for returning success or failure from
operations so callers branch on `IsSuccess` / `IsFailure` (and `Status` /
`Problems`) instead of driving control flow with exceptions.

## Key ideas

- Return a `Result` or `Result<T>` from an operation; do not throw for expected
  outcomes (not found, validation failure, conflict, and so on).
- Branch on `IsSuccess` / `IsFailure`, or use `Map` to fold both paths into one
  value.
- Attach structured `IProblem` details on failure; keep status explicit via
  `ResultStatus`.
- `ToStatusCode()` / `StatusCode()` map statuses to conventional HTTP codes when
  you bridge to an API layer — the library itself has no ASP.NET dependency.

## How to run

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run the demo

```bash
dotnet run --project Patterns.Result.Demo
```

Expected output shows one success path (`ada`) and one not-found failure
(`unknown`).

### Use the library

Reference `Patterns.Result` from your project (copy the folder, or add a project
reference). Namespace: `Patterns.Result`.

```csharp
Result<User> result = LookupUser(id);

if (result.IsFailure)
{
    // inspect result.Status and result.Problems
    return;
}

User user = result.Value;
```

## Layout

| Path | Role |
|------|------|
| `Patterns.Result/` | Result kit (types + extensions) |
| `Patterns.Result.Demo/` | Thin console showing success/failure branching |
| `Patterns.Result.slnx` | Solution for both projects |
