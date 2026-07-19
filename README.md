# dev-codex

A curated collection of my software development knowledge, patterns, and best practices — reference implementations for learning and reuse.

## About

`dev-codex` is a centralized hub for my coding demos, reusable patterns, and development best practices. Each sub-project (or "node") is a self-contained example focused on a single concept, pattern, or technique.

The goal is twofold:

1. **A public showcase** of my personal coding style and approach.
2. **A personal reference** I (and any AI agents I work with) can pull from when building new applications.

## Repository Structure

Each concept lives in its own top-level folder with its own README explaining what it demonstrates:

```
dev-codex/
├── mvc-todo-app/
├── extension-methods/
├── repository-design-pattern/
└── ...
```

## Index

| Node | Description |
|------|-------------|
| _MVC Todo App_ | A basic CRUD app demonstrating the MVC pattern. |
| _Extension Methods_ | Examples of extending existing types cleanly. |
| _Repository Design Pattern_ | Abstracting data access behind a repository. |
| _..._ | _More coming soon._ |

## How to Use

1. Browse the folders above (or the index table) to find a concept.
2. Open that folder's `README.md` for an explanation and usage notes.
3. Copy, adapt, or learn from the code as needed.

## Project Management

Repo-wide planning, tracking, and decisions live at the project level — not inside nodes:

- [`docs/roadmap.md`](docs/roadmap.md) — what's planned, in progress, and done.
- [`docs/decisions/`](docs/decisions/) — Architecture Decision Records (ADRs).
- [`docs/conventions.md`](docs/conventions.md) — expanded conventions and node structure.
- [`CHANGELOG.md`](CHANGELOG.md) — repo-wide evolution log.
- [`AGENTS.md`](AGENTS.md) — portable guidance for AI-assisted work (with Cursor-native rules in `.cursor/rules/`).

## Conventions

- One concept per folder.
- Every folder includes its own `README.md`.
- Code favors clarity over cleverness — these are teaching/reference examples.
- **Nodes stay self-contained**: a node documents only itself. All project-level planning, decisions, and change tracking live at the repo root (see [Project Management](#project-management)).

## Author

**Jeremy Chambers**

## License

Released under the [MIT License](LICENSE).
