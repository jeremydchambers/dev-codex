# dev-codex

A curated collection of my software development knowledge, patterns, and best practices — reference implementations for learning and reuse.

## About

`dev-codex` is a centralized hub for my coding demos, reusable patterns, and development best practices. Each sub-project (or "node") is a self-contained example focused on a single concept, pattern, or technique.

The goal is twofold:

1. **A public showcase** of my personal coding style and approach.
2. **A personal reference** I (and any AI agents I work with) can pull from when building new applications.

## Repository Structure

Each concept lives in its own top-level folder with its own README explaining what it demonstrates:

```
dev-codex/
├── azure-functions--durable/
├── azure-service-bus--why-messaging/
├── azure-key-vault--secrets/
└── ...
```

## Index

| Node | Description |
|------|-------------|
| [AI Workflows — AI Hero Tools](ai-workflows--ai-hero-tools/) | Crib sheet for Matt Pocock’s AI Hero skills: idea→ship map + local-repo walkthrough; defers routing to `/ask-matt`. |
| [Azure Key Vault Secrets](azure-key-vault--secrets/) | .NET console app reads a Secret via `SecretClient` + `DefaultAzureCredential`. |
| [Azure Functions — Triggers & Bindings](azure-functions--triggers-bindings/) | Trigger/binding model: HTTP, Timer, Service Bus, Queue, Blob; input vs output. |
| [Azure Functions — Hosting Plans](azure-functions--hosting-plans/) | Consumption / Flex / Premium / Dedicated and cold-start trade-offs. |
| [Azure Functions — Isolated Worker](azure-functions--isolated-worker/) | .NET isolated vs in-process; why isolated is the default. |
| [Azure Functions — Durable (Conceptual)](azure-functions--durable/) | Orchestrator / activity / entity, fan-out/fan-in, when to use Durable. |
| [Azure Functions — Practice](azure-functions--practice/) | .NET isolated HTTP + Service Bus app with DI, ILogger, app settings, Flex deploy. |
| [Azure Functions — Best Practices](azure-functions--best-practices/) | Functions vs API endpoint; series checklist and cross-cutting tips. |
| [Azure Service Bus — Why Messaging](azure-service-bus--why-messaging/) | Decoupling, load leveling, resilience — producers and consumers succeed independently. |
| [Azure Service Bus — Queues vs Topics](azure-service-bus--queues-topics/) | Point-to-point vs pub/sub; one consuming app vs independent subscribers. |
| [Azure Service Bus — Reliability](azure-service-bus--reliability/) | At-least-once, PeekLock, retries, DLQ, idempotency checklist. |
| [Azure Service Bus — Sessions](azure-service-bus--sessions/) | Competing consumers for scale; sessions for ordered subsets. |
| [Azure Service Bus — Practice](azure-service-bus--practice/) | .NET 10 isolated: HTTP enqueue → queue process → DLQ path; optional MI + RBAC. |

## How to Use

1. Browse the folders above (or the index table) to find a concept.
2. Open that folder's `README.md` for an explanation and usage notes.
3. Copy, adapt, or learn from the code as needed.

## Project Management

Repo-wide planning, tracking, and decisions live at the project level — not inside nodes:

- [`docs/roadmap.md`](docs/roadmap.md) — what's planned, in progress, and done.
- [`docs/decisions/`](docs/decisions/) — Architecture Decision Records (ADRs).
- [`docs/conventions.md`](docs/conventions.md) — expanded conventions and node structure.
- [`CHANGELOG.md`](CHANGELOG.md) — repo-wide evolution log.
- [`AGENTS.md`](AGENTS.md) — portable guidance for AI-assisted work (with Cursor-native rules in `.cursor/rules/`).

## Conventions

- One concept per folder.
- Every folder includes its own `README.md`.
- Code favors clarity over cleverness — these are teaching/reference examples.
- **Nodes stay self-contained**: a node documents only itself. All project-level planning, decisions, and change tracking live at the repo root (see [Project Management](#project-management)).

## Author

**Jeremy Chambers**

## License

Released under the [MIT License](LICENSE).
