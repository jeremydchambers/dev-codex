# AGENTS.md

Portable, tool-agnostic guidance for AI agents (and humans) working in `dev-codex`.
This file is the **source of truth** for how we work. It is intentionally written in
plain Markdown so it stays useful across any AI tool. Cursor-specific, glob-scoped
behavior lives in `.cursor/rules/*.mdc` and layers on top of this file.

## What this repo is

`dev-codex` is a curated collection of self-contained coding demos. Each top-level
folder is a **node**: one concept, pattern, or technique, documented by its own
`README.md`. It serves as both a public showcase of coding style and a personal
(and AI) reference for building new applications.

## Two-tier responsibility model

Keep these layers separate. When in doubt about where something belongs, use this split.

- **Project level (repo root):** planning, tracking, and documenting how the repo
  evolves. Lives in `docs/` (roadmap, conventions, decisions), `CHANGELOG.md`, this
  file, and `README.md`.
- **Node level (each folder):** the concept itself — its explanation, code, and usage
  notes. A node documents **only itself**. It never owns project-management material.

Rule of thumb: *project level answers "how do we work and how did this repo evolve";
node level answers "what does this one concept demonstrate."*

## Conventions

- **One concept per folder.** Keep nodes independent and copy-pasteable.
- **Every node has its own `README.md`** explaining what it demonstrates and how to run it.
- **Clarity over cleverness.** These are teaching/reference examples; favor readable code.
- **Nodes stay self-contained.** No cross-node imports; no project-level docs inside a node.
- **Keep the root index current.** When adding/removing a node, update the Index table in `README.md`.

## How to add a new node

1. Create a top-level folder named for the concept (kebab-case, e.g. `repository-design-pattern`).
2. Add a `README.md` to that folder covering: what it demonstrates, key ideas, how to run it.
3. Add the node to the **Index** table in the root `README.md`.
4. Record noteworthy structural or directional decisions as an ADR in `docs/decisions/`.
5. Add a line to `CHANGELOG.md` describing the addition.

## Project management (where the process lives)

- `docs/roadmap.md` — what's planned, in progress, and done.
- `docs/conventions.md` — expanded conventions and node structure details.
- `docs/decisions/` — Architecture Decision Records (ADRs), one file per significant decision.
- `docs/agents/` — runtime config for installed agent skills (issue tracker, triage, domain).
- `CHANGELOG.md` — repo-wide, human-readable evolution log.

## Agent skills

Project-level skills live under `.agents/skills/`, pinned by `skills-lock.json`.
Install or refresh with `npx skills@latest add mattpocock/skills`. Skills are AI workflow
config (ADR 0003), not demo nodes — do not add them to the README Index.

### Issue tracker

Local markdown under `.scratch/<feature-slug>/`. See `docs/agents/issue-tracker.md`.

### Triage labels

Default five roles (`needs-triage`, `needs-info`, `ready-for-agent`, `ready-for-human`,
`wontfix`) recorded as each issue file's `Status:` line. See `docs/agents/triage-labels.md`.

### Domain docs

Single-context: optional root `CONTEXT.md`; ADRs in `docs/decisions/`. See
`docs/agents/domain.md`.

## Commit style

- Short, imperative subject lines (e.g. `Add repository-design-pattern node`).
- Group related changes; keep node changes and project-level changes reasonably separate.
- Reference an ADR when a commit implements a recorded decision.
