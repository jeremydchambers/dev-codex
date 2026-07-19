# Conventions

Expanded conventions for `dev-codex`. `AGENTS.md` holds the concise, portable summary;
this file is the fuller reference. When a decision changes a convention, update it here
and link the relevant ADR in `docs/decisions/`.

## Node structure

Each node is a self-contained top-level folder:

```
<concept-name>/
└── README.md        # required: what it demonstrates + how to use it
    ...              # source files for the concept
```

- **Naming:** kebab-case folder names (`repository-design-pattern`).
- **Independence:** no cross-node imports; a node must make sense in isolation.
- **Docs scope:** a node documents only itself. Project-level material lives at the root.

## Node README shape

1. **Title** — the concept name.
2. **What it demonstrates** — 1–3 sentences.
3. **Key ideas** — the takeaways.
4. **How to run / use** — steps or commands, if applicable.

Optional extra sections (`## Series`, `## References`, `## Layout`, etc.) are fine
when they help the reader; they do not replace the four sections above.

## Multi-node series

When one topic is too broad for a single readable node, split it into several
top-level nodes that share a naming prefix and an ordered learning path
(see [ADR 0004](decisions/0004-azure-functions-series.md)).

- **Still one concept per folder.** Each series member is a normal independent
  node with its own README (and code, if any).
- **Links only.** Series navigation may link sibling nodes; do **not** add
  cross-node code imports.
- **Series nav in each README.** Include a short `## Series` block (ordered
  list + prev/next) so readers can walk the path without relying on the root
  Index alone.
- **Index lists every member.** Each series node gets its own row in the root
  Index — do not collapse a series into one Index entry or one fat folder.

## Root README index

The root `README.md` maintains an **Index** table of all nodes. Update it whenever a
node is added or removed.

## Two-tier responsibility split

- **Project level (root):** roadmap, conventions, decisions (ADRs), changelog, AI config.
- **Node level (folder):** the concept, its code, and node-specific usage notes.

See `AGENTS.md` for the quick decision rule.

## AI workflow configuration

Three layers, from portable core to tool-specific (see ADRs 0002 and 0003):

- **`AGENTS.md`** — portable, tool-agnostic source of truth (always applies). Includes
  the `## Agent skills` summary pointing at `docs/agents/`.
- **`.agents/skills/` + `skills-lock.json`** — project-level Agent Skills (workflows
  such as triage, TDD, domain modeling). Install/refresh with
  `npx skills@latest add mattpocock/skills`. Skill runtime config lives in
  `docs/agents/`. Skills are **not** demo nodes — they stay out of the README Index.
- **`.cursor/rules/*.mdc`** — Cursor-native layer for glob-scoped rules that Markdown
  can't express (e.g. language-specific conventions attached only to matching files).
