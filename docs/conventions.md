# Conventions

Expanded conventions for `dev-codex`. `AGENTS.md` holds the concise, portable summary;
this file is the fuller reference. When a decision changes a convention, update it here
and link the relevant ADR in `docs/decisions/`.

## Node structure

Each node is a self-contained top-level folder:

```
<series>--<topic>/
└── README.md        # required: what it demonstrates + how to use it
    ...              # source files for the concept
```

- **Naming:** `{series}--{topic}` with exactly one `--`
  (e.g. `azure-functions--durable`). Both sides are required non-empty kebab-case.
  See [ADR 0008](decisions/0008-series-topic-folder-naming.md).
- **Independence:** no cross-node imports; a node must make sense in isolation.
- **Docs scope:** a node documents only itself. Project-level material lives at the root.

## Node README shape

1. **Title** — the concept name.
2. **What it demonstrates** — 1–3 sentences.
3. **Key ideas** — the takeaways.
4. **How to run / use** — steps or commands, if applicable.

Optional extra sections (`## Series`, `## References`, `## Layout`, etc.) are fine
when they help the reader; they do not replace the four sections above.

## Series and multi-node learning paths

A **Series** is a named group of Nodes. Every Node folder uses
`{series}--{topic}`; the left segment is the Series name. Extending a Series
must match an existing left segment exactly (authority is on-disk names — no
separate registry). Unbuilt Index placeholders do not invent a Series until a
folder is created ([ADR 0008](decisions/0008-series-topic-folder-naming.md)).

When one topic is too broad for a single readable node, split it into several
top-level nodes in the same Series and optionally add an ordered learning path
(see [ADR 0004](decisions/0004-azure-functions-series.md)).

- **Still one concept per folder.** Each Series member is a normal independent
  node with its own README (and code, if any).
- **Links only.** Series navigation may link sibling nodes; do **not** add
  cross-node code imports.
- **Ordered nav is optional.** When there is a teaching sequence, include a
  short `## Series` block (ordered list + prev/next) so readers can walk the
  path without relying on the root Index alone. `--` does not replace that nav.
- **Index lists every member.** Each Series node gets its own row in the root
  Index — do not collapse a Series into one Index entry or one fat folder.

## Root README index

The root `README.md` maintains an **Index** table of all nodes. Update it whenever a
node is added or removed.

## Two-tier responsibility split

- **Project level (root):** roadmap, conventions, decisions (ADRs), changelog, AI config.
- **Node level (folder):** the concept, its code, and node-specific usage notes.

See `AGENTS.md` for the quick decision rule.

## .NET nodes (Standard target framework)

Every runnable .NET Node uses the same **Standard target framework**: `net10.0`
([ADR 0007](decisions/0007-dotnet-10-repo-standard.md)). No per-node exceptions.

| Mechanism | Role |
|-----------|------|
| Root `Directory.Build.props` | Sets `TargetFramework` to `net10.0`; individual `.csproj` files omit it and inherit |
| Root `global.json` | Requires a .NET 10 SDK with `rollForward: latestMajor` |
| Node READMEs | Prerequisites say .NET 10 SDK; Azure Functions Flex demos use `--runtime-version 10.0` |

Package versions stay declared per node (no central `Directory.Packages.props`).
When copying a node out of the repo, either keep the root props/SDK files in the
copy set or re-add an explicit `TargetFramework` to each `.csproj`.

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
