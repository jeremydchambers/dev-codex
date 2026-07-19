# Architecture Decision Records (ADRs)

This folder captures significant decisions about how `dev-codex` is structured and
evolves. Each ADR is a short, dated record: **context → decision → consequences**.

## Rules

- One decision per file, named `NNNN-short-title.md` (zero-padded, sequential).
- ADRs are immutable once **Accepted**. To change a decision, add a new ADR that
  **supersedes** the old one and update the old one's status.
- Use `0000-template.md` as the starting point for new ADRs.

## Index

| ADR | Title | Status |
|-----|-------|--------|
| [0001](0001-node-based-structure.md) | Node-based repository structure | Accepted |
| [0002](0002-project-vs-node-responsibilities.md) | Project-level vs node-level responsibilities | Accepted |
| [0003](0003-project-level-agent-skills.md) | Project-level agent skills | Accepted |
| [0004](0004-azure-functions-series.md) | Azure Functions learning series (six nodes) | Accepted |
| [0005](0005-azure-service-bus-series.md) | Azure Service Bus learning series (five nodes) | Accepted |
| [0006](0006-azure-service-bus-practice-details.md) | Azure Service Bus practice delivery details | Accepted |
| [0007](0007-dotnet-10-repo-standard.md) | .NET 10 as the repo-wide standard for .NET nodes | Accepted |
| [0008](0008-series-topic-folder-naming.md) | Node folder names are `series--topic` | Accepted |
