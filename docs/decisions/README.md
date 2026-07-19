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
