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

## Root README index

The root `README.md` maintains an **Index** table of all nodes. Update it whenever a
node is added or removed.

## Two-tier responsibility split

- **Project level (root):** roadmap, conventions, decisions (ADRs), changelog, AI config.
- **Node level (folder):** the concept, its code, and node-specific usage notes.

See `AGENTS.md` for the quick decision rule.

## AI workflow configuration

- **`AGENTS.md`** — portable, tool-agnostic source of truth (always applies).
- **`.cursor/rules/*.mdc`** — Cursor-native layer for glob-scoped rules that Markdown
  can't express (e.g. language-specific conventions attached only to matching files).
