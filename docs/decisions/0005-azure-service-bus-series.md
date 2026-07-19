# 0005. Azure Service Bus learning series (five nodes)

- **Status:** Accepted — net10 vs Functions-practice net8 “intentional divergence”
  **superseded in part by** [0007](0007-dotnet-10-repo-standard.md) (series spine
  and queue-only practice remain)
- **Date:** 2026-07-19

## Context

Azure Service Bus spans several distinct teaching goals (why messaging, queues vs
topics, reliability semantics, sessions/competing consumers, and a runnable practice
loop). A single fat node would fight the one-concept-per-folder rule. Learners still
need an ordered path. An existing Functions practice node already shows a light
Service Bus receive path; this series needs a deeper broker-first spine without
rewriting that node.

## Decision

Ship a **five-node series** under the `azure-service-bus-*` prefix:

1. `azure-service-bus-why-messaging` — motivation; one-liner: messaging lets producers
   and consumers succeed independently (they need not be ready, healthy, or scaled
   together)
2. `azure-service-bus-queues-topics` — point-to-point vs pub/sub; queue = one consuming
   *application* (competing instances OK); topic = fan-out to *independent* subscribing
   applications
3. `azure-service-bus-reliability` — at-least-once, idempotency, DLQ, retries/back-off,
   PeekLock vs ReceiveAndDelete (recommend PeekLock almost always)
4. `azure-service-bus-sessions` — equal weight: competing consumers and
   sessions/ordering
5. `azure-service-bus-practice` — .NET **10** isolated Function App: HTTP publish +
   queue consume; PeekLock + failure → DLQ; idempotency as comment/checklist; connection
   string locally; optional Azure notes with MI + RBAC

Each README includes **Series** prev/next nav (links only; no cross-node code imports).
Nodes 1–4 are README-only; runnable code lives only in practice. Teaching is
**broker-first**; Functions host details stay light asides plus practice. Capstone is
practice — no sixth best-practices node. Light cross-links to the Functions series;
leave `azure-functions-practice` unchanged. Out of scope for v1: premium features
(partitioning, duplicate detection, geo-DR), Event Hubs comparisons, JMS.

Practice uses a **queue only** (topics remain conceptual). Runtime **net10** is an
intentional divergence from Functions practice’s net8/Flex story.

## Consequences

- Clear Service Bus learning path parallel to ADR 0004’s Functions series shape.
- Index grows by five entries; Series nav carries order.
- Functions practice stays the lighter SB receive example; SB practice owns the
  publish → process → DLQ teaching loop.
- Readers may wonder why SB practice is on .NET 10 while Functions practice stays on
  net8 — that split is deliberate and recorded here.
