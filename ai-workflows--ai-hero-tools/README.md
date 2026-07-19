# AI Workflows — AI Hero Tools

## What it demonstrates

A thin, human-facing map of [Matt Pocock’s AI Hero engineering skills](https://github.com/mattpocock/skills) as installed in this repo: which stage of the **idea → ship** path pulls which skill, and how that path looks against *this* repo’s paths — without rewriting the skills themselves.

For full routing (“which skill fits my situation?”), use **`/ask-matt`**. This node is a crib sheet companion, not a competing source of truth. The skill packages stay project-scoped ([ADR 0003](../docs/decisions/0003-project-level-agent-skills.md)); this Node only teaches the *usage pattern*.

## Before you start

If `docs/agents/` is not configured yet, run **`/setup-matt-pocock-skills`** once. This repo’s runtime config lives under `docs/agents/` (issue tracker, triage labels, domain docs) and is summarized in `AGENTS.md`.

## Key ideas

### Main flow: idea → ship

| Stage | Skill | When |
|-------|--------|------|
| Sharpen the idea | `/grill-with-docs` | You have a codebase and want decisions retained in `CONTEXT.md` / ADRs |
| Answer a design question by running something | `/handoff` → `/prototype` → `/handoff` | Conversation alone can’t settle state, logic, or UI |
| Multi-session build | `/to-spec` → `/to-tickets` | Work won’t fit one context window; tickets carry blocking edges |
| Same-session build | `/implement` | Scope is small enough to finish without ticketing |
| Build each ticket | `/implement` (drives `/tdd`, then `/code-review`) | Fresh context per ticket after `/to-tickets` |
| Context hygiene | Keep grill → spec → tickets in one window; `/handoff` before you blow the [smart zone](https://www.aihero.dev/ai-coding-dictionary/smart-zone) | Don’t compact mid-phase; fork with `/handoff` when you need a fresh session |

**Fork after grilling:** if you can settle everything in conversation and the work is small → `/implement` here. If it’s a multi-session build → `/to-spec`, then `/to-tickets`, then `/implement` per ticket (clear context between tickets).

### Also exists (pointers only)

| Situation | Skill |
|-----------|--------|
| Incoming bugs / requests you didn’t author | `/triage` → later `/implement` |
| Hard / flaky / regression bugs | `/diagnosing-bugs` |
| Huge foggy effort (greenfield or oversized feature) | `/wayfinder` → then merge at `/to-spec` |
| Spare-moment codebase deepening | `/improve-codebase-architecture` (uses `/codebase-design`) |
| Cross-session bridge / same-thread summarize | `/handoff` / built-in `/compact` |
| No codebase | `/grill-me` (stateless cousin of `/grill-with-docs`) |
| Background reading | `/research` |
| Learn a concept over sessions | `/teach` |
| Write or edit skills well | `/writing-great-skills` |

Vocabulary layers that run underneath when skills need them: `/domain-modeling`, `/codebase-design`. Details live in `/ask-matt`.

### Hypothetical walkthrough (this repo)

**Feature (imaginary — do not create tickets):** add optional `## Series` Prev/Next nav to an existing Series that doesn’t have an ordered path yet.

1. **`/grill-with-docs`** — settle scope: which Series, whether nav is optional forever, how Index vs folder nav relate (ADR 0008 already covers naming). Domain terms stay in `CONTEXT.md` only if something new crystallizes; most of this is convention, already documented.
2. **Fork** — for a docs-only README tweak on one Series, stay in-session and **`/implement`**. You would only take **`/to-spec` → `/to-tickets`** if the change grew (e.g. every Series in the Index, plus conventions + changelog + a migration checklist across many folders).
3. **If you had ticketed** — files would land under `.scratch/<feature-slug>/` (`spec.md`, `issues/01-….md`, …) per `docs/agents/issue-tracker.md`. Each `/implement` would start fresh from one issue file.
4. **Close** — `/implement` finishes with `/code-review` (Standards + Spec) before commit. Project-level Index / `CHANGELOG.md` updates stay at the repo root, not inside a Node.

Real paths this walkthrough assumes: `CONTEXT.md`, `docs/agents/`, `docs/decisions/`, `.scratch/<feature-slug>/`. No live tracker files are part of this Node.

## How to use

1. Skim the main-flow table above when you start a piece of work.
2. Invoke **`/ask-matt`** if you’re unsure which branch or on-ramp applies.
3. Open the matching skill under `.agents/skills/<name>/SKILL.md` only when you need the full procedure — don’t copy those docs into a Node.
4. Prefer this README as a human crib sheet; prefer `/ask-matt` as the agent router.

## References

- Installed skills: `.agents/skills/` (pinned by `skills-lock.json`)
- Skill runtime config: [`docs/agents/`](../docs/agents/)
- Upstream pack: [mattpocock/skills](https://github.com/mattpocock/skills)
- ADR: [0003 — Project-level agent skills](../docs/decisions/0003-project-level-agent-skills.md)
