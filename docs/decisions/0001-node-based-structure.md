# 0001. Node-based repository structure

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

`dev-codex` is a personal codex of coding demos, patterns, and best practices, intended
as both a public showcase and a reference for building new applications (by me and by AI
agents). The concepts span multiple languages and frameworks and will grow over time.

## Decision

Organize the repo as a flat collection of self-contained **nodes**: one top-level folder
per concept, each with its own `README.md`. Nodes are independent — no cross-node imports —
so any node can be understood, copied, or reused in isolation.

## Consequences

- Easy to browse, add to, and reuse individual concepts.
- Each node needs its own README, creating light per-node overhead.
- Shared conventions must live at the project level to avoid duplication across nodes
  (see ADR 0002).
