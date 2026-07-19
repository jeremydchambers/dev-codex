# Changelog

Repo-wide, human-readable log of how `dev-codex` evolves. Add an entry when a node is
added or removed, or when project-level structure changes. Newest entries on top.

The format is loosely based on [Keep a Changelog](https://keepachangelog.com/).

## [Unreleased]

### Added

- Azure Service Bus learning series (ADR 0005): five `azure-service-bus-*` nodes —
  why-messaging, queues-topics, reliability, sessions, and practice (.NET 10
  isolated HTTP → queue → DLQ). Practice delivery details in ADR 0006
  (unit-tested services, Flex MI/RBAC runbook, Series glossary).
- Azure Functions learning series (ADR 0004): six `azure-functions-*` nodes —
  triggers/bindings, hosting plans, isolated worker, Durable (conceptual),
  practice (HTTP + Service Bus, Flex Consumption + MI), and best practices.
- `azure-key-vault-secrets` node — .NET 10 console app reads a Secret from Azure
  Key Vault with `SecretClient` and `DefaultAzureCredential` (RBAC runbook;
  value presence checked, never printed).
- Project-management scaffolding: `AGENTS.md`, `docs/` (roadmap, conventions,
  decisions), and this `CHANGELOG.md`.
- Cursor-native rules under `.cursor/rules/` for glob-scoped guidance.
- ADRs 0001 (node-based structure), 0002 (project vs node responsibilities), and
  0003 (project-level agent skills).
- Matt Pocock agent skills under `.agents/skills/` (pinned by `skills-lock.json`),
  with skill runtime config in `docs/agents/` (local-markdown issue tracker,
  default triage labels, single-context domain docs pointing at `docs/decisions/`).
