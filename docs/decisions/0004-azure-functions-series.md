# 0004. Azure Functions learning series (six nodes)

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

Azure Functions spans several distinct concepts (triggers/bindings, hosting, .NET
process model, Durable orchestration, hands-on composition, and “when to use”).
Cramming all of that into a single node fights the one-concept-per-folder rule and
produces an unreadable README. The learner still needs an ordered path and runnable
practice.

## Decision

Ship a **six-node series** under the `azure-functions-*` prefix:

1. `azure-functions-triggers-bindings` — trigger/binding model (conceptual)
2. `azure-functions-hosting-plans` — Consumption / Flex / Premium / Dedicated + cold start
3. `azure-functions-isolated-worker` — isolated vs in-process; why isolated is default
4. `azure-functions-durable` — Durable concepts only (no Durable host project)
5. `azure-functions-practice` — .NET isolated app: HTTP + Service Bus, DI, logging, config, tests, az CLI → Flex Consumption with MI+RBAC for Service Bus
6. `azure-functions-best-practices` — Functions vs API + series checklist

Each node README includes a **Series** prev/next nav (links only; still no
cross-node code imports). Practice targets **.NET isolated** on **net8.0** to match
documented Flex Consumption `--runtime-version 8.0`.

## Consequences

- Clear learning path mapped to interview/learning goals.
- Index grows by six entries; readers discover order via Series nav and root Index.
- Durable stays conceptual for now — a future node can add a thin orchestrator sample
  without rewriting the series spine.
- Flex Consumption (not legacy Consumption) is what Practice provisions, while Hosting
  Plans still teaches the classic Consumption / Premium / App Service vocabulary.
