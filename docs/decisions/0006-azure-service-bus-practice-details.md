# 0006. Azure Service Bus practice delivery details

- **Status:** Accepted — Series glossary wording in decision §3 **superseded in part by**
  [0008](0008-series-topic-folder-naming.md) (Series = named group; order optional)
- **Date:** 2026-07-19
- **Clarifies:** [0005](0005-azure-service-bus-series.md)

## Context

ADR 0005 defined the five-node Service Bus series spine. Implementation also
kept a few delivery details that a narrow reading of 0005’s practice bullet
could treat as optional extras. Those details are intentional and should be
recorded so reviews don’t flag them as scope creep.

## Decision

For `azure-service-bus--practice` (and related glossary):

1. **Unit-tested services** — pure enqueue/process logic has xUnit coverage at
   service seams (same teaching pattern as Functions practice).
2. **Optional Azure path** — README may include a Flex Consumption create →
   identity → RBAC → publish runbook in bash and PowerShell (not connection-string–
   only notes).
3. **Series glossary** — root `CONTEXT.md` defines **Series** as ordered Nodes
   with Series/Prev/Next navigation.

The five-node spine, broker-first teaching, queue-only practice, and .NET 10
isolated runtime from ADR 0005 are unchanged.

## Consequences

- Practice stays copy-paste runnable for local and Azure identity paths.
- Domain language for multi-node series is shared repo-wide via `CONTEXT.md`.
