# 0008. Node folder names are `series--topic`

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

Nodes already group into Series via a shared kebab-case prefix
(`azure-functions-*`, `azure-service-bus-*`), but a single `-` does not mark where
the Series name ends and the topic begins. Humans and agents must guess the
boundary (`azure-service-bus-queues-topics`). Nested folders would clarify
grouping but would fight the flat node model (ADR 0001). Plain kebab alone stays
ambiguous as the codex grows.

## Decision

1. **Every on-disk Node folder** is named `{series}--{topic}` with **exactly one**
   `--` delimiter. Both sides are required, non-empty, kebab-case (hyphens allowed
   within each side).
2. **Series** (glossary) is a named group of Nodes (possibly one). An ordered
   learning path with `## Series` prev/next is optional — only when there is a
   teaching sequence.
3. **`--` is folder naming only.** It does not replace root Index rows or
   `## Series` navigation. Renames update those path links in the same change.
4. **Series name authority** is the left segment of existing folders. Extending a
   Series must match that spelling exactly. No separate Series registry.
5. **Unbuilt Index placeholders** do not invent a Series until a folder is created.
6. **Document** in `CONTEXT.md` and `docs/conventions.md`. Path lists in
   [ADR 0004](0004-azure-functions-series.md) and
   [ADR 0005](0005-azure-service-bus-series.md) remain historical;
   naming going forward follows this ADR.

### Rename map (migration)

| Before | After |
|--------|-------|
| `azure-functions-triggers-bindings` | `azure-functions--triggers-bindings` |
| `azure-functions-hosting-plans` | `azure-functions--hosting-plans` |
| `azure-functions-isolated-worker` | `azure-functions--isolated-worker` |
| `azure-functions-durable` | `azure-functions--durable` |
| `azure-functions-practice` | `azure-functions--practice` |
| `azure-functions-best-practices` | `azure-functions--best-practices` |
| `azure-service-bus-why-messaging` | `azure-service-bus--why-messaging` |
| `azure-service-bus-queues-topics` | `azure-service-bus--queues-topics` |
| `azure-service-bus-reliability` | `azure-service-bus--reliability` |
| `azure-service-bus-sessions` | `azure-service-bus--sessions` |
| `azure-service-bus-practice` | `azure-service-bus--practice` |
| `azure-key-vault-secrets` | `azure-key-vault--secrets` |

## Consequences

- Folder names encode Series membership without nested directories.
- All Index and Series-nav hrefs must move with the folders.
- Historical ADR path strings stay as written; readers follow this ADR for
  current names.
- Display titles may keep an em dash (—); folders use `--`.
