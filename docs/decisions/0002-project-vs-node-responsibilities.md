# 0002. Project-level vs node-level responsibilities

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

With a node-based structure (ADR 0001), we need a clear home for planning, tracking, and
documenting how the repo evolves — without coupling that process into the independent nodes.
We also want AI coding workflows that work well in Cursor today while staying portable to
other tools in the future.

## Decision

Adopt a **two-tier responsibility model**:

- **Project level (repo root):** planning, change tracking, decisions, conventions, and AI
  workflow config — `docs/` (roadmap, conventions, decisions), `CHANGELOG.md`, `AGENTS.md`,
  and the root `README.md`.
- **Node level (each folder):** the concept itself — explanation, code, and usage notes.
  A node documents only itself and owns no project-management material.

For AI configuration, use a **layered hybrid**:

- `AGENTS.md` as the portable, tool-agnostic source of truth (durable core guidance).
- `.cursor/rules/*.mdc` as the Cursor-native layer for glob-scoped rules that Markdown
  cannot express (e.g. language-specific conventions attached only to matching files).

## Consequences

- Clear separation keeps nodes self-contained and the process easy to find.
- Core guidance survives a tool change; only the Cursor-specific scoping layer is tool-bound.
- Some guidance is intentionally split across two files, so the boundary
  ("portable core" vs "scoped activation") must be kept clean to avoid duplication.
