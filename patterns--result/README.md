# Result (control-flow utility)

## What it demonstrates

A small C# `Result` / `Result<T>` kit for returning success or failure from
operations so callers branch on `IsSuccess` / `IsFailure` (and `Error`) instead
of driving control flow with exceptions.

## Key ideas

- Return a `Result` or `Result<T>` from an operation; do not throw for expected
  outcomes (not found, validation failure, conflict, and so on).
- Branch on `IsSuccess` / `IsFailure`, or use `Map` to fold both paths into one
  value.
- A failure carries one `Error` string. Structured problem types can wait until
  you copy the folder and need them.

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
    // inspect result.Error
    return;
}

User user = result.Value;
```

## Layout

| Path | Role |
|------|------|
| `Patterns.Result/Result.cs` | No-value `Result` |
| `Patterns.Result/ResultOfT.cs` | `Result<T>` |
| `Patterns.Result.Demo/` | Thin console showing `Map` over success/failure |
| `Patterns.Result.slnx` | Solution for both projects |
