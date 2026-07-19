# 0003. Project-level agent skills

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

We want repeatable AI engineering workflows (triage, TDD, domain modeling, grilling,
prototyping, etc.) that agents can invoke consistently. Those workflows belong at the
**project** tier (ADR 0002): how we work, not what any single demo node teaches.

The [mattpocock/skills](https://github.com/mattpocock/skills) collection provides those  
workflows as installable Agent Skills. They need a small amount of per-repo config  
(issue tracker, triage vocabulary, domain-doc layout) so skills know where to read and write.   
  
mattpocock/skills: A complete AI Coding workflow, end-to-end : [https://youtu.be/M6mYodf0dJM?si=IbTcdQcvBUR3JUq6](https://youtu.be/M6mYodf0dJM?si=IbTcdQcvBUR3JUq6)

## Decision

Adopt **project-level agent skills** as a third AI-config layer alongside `AGENTS.md` and
`.cursor/rules/`:

1. **Install** skills with the `skills` CLI into `.agents/skills/`, pinned by
  `skills-lock.json` at the repo root:
2. **Configure** skill runtime expectations under `docs/agents/` (issue tracker, triage
  labels, domain docs) and summarize them in an `## Agent skills` section of `AGENTS.md`.
   Prefer the `setup-matt-pocock-skills` skill for initial setup.
3. **Keep skills project-scoped** — they are not demo nodes. Do not add them to the root
  README Index or give them a concept-folder layout.
4. **Domain ADRs stay in `docs/decisions/`** — this repo's existing ADR home. Skill
  consumers are told that path via `docs/agents/domain.md` (not the upstream default
   `docs/adr/`).

## Consequences

- Agents share the same engineering playbooks across sessions and tools that honor
`.agents/skills/`.
- Version pins in `skills-lock.json` make updates intentional (`npx skills@latest …`
again when refreshing).
- Skill-generated tickets live under `.scratch/` (local markdown tracker); they must not
be mistaken for node documentation.
- Conventions and changelog should mention the skills layer whenever it changes (install,
tracker switch, major refresh).

