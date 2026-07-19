# Changelog

Repo-wide, human-readable log of how `dev-codex` evolves. Add an entry when a node is
added or removed, or when project-level structure changes. Newest entries on top.

The format is loosely based on [Keep a Changelog](https://keepachangelog.com/).

## [Unreleased]

### Added

- Project-management scaffolding: `AGENTS.md`, `docs/` (roadmap, conventions,
  decisions), and this `CHANGELOG.md`.
- Cursor-native rules under `.cursor/rules/` for glob-scoped guidance.
- ADRs 0001 (node-based structure), 0002 (project vs node responsibilities), and
  0003 (project-level agent skills).
- Matt Pocock agent skills under `.agents/skills/` (pinned by `skills-lock.json`),
  with skill runtime config in `docs/agents/` (local-markdown issue tracker,
  default triage labels, single-context domain docs pointing at `docs/decisions/`).
