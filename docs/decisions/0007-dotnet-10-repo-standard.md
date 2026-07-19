# 0007. .NET 10 as the repo-wide standard for .NET nodes

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

This repo has multiple .NET nodes. Today their target frameworks diverge:
`azure-key-vault-secrets` and `azure-service-bus-practice` use `net10.0`, while
`azure-functions-practice` uses `net8.0`. ADR 0004 tied Functions practice to net8
to match Flex Consumption `--runtime-version 8.0`. ADR 0005 recorded Service Bus
practice on net10 as an intentional divergence from that story.

We want one standard so agents and humans do not guess which TFM or SDK a node
expects, and so Flex + isolated demos stay aligned with current platform support
(.NET 10 is supported on Flex Consumption for the isolated worker).

## Decision

1. **Standard target framework is `net10.0` for every .NET node** — current and
   future. No per-node TFM exceptions.
2. **Enforce at the project tier** with a root `Directory.Build.props` that sets
   `TargetFramework` to `net10.0`. Individual `.csproj` files omit
   `TargetFramework` and inherit it.
3. **Pin the SDK** with a root `global.json` requiring a .NET 10 SDK and
   `rollForward: latestMajor` (stay on major 10; allow patch/feature bumps).
4. **Document** the rule in `docs/conventions.md` and a short pointer in
   `AGENTS.md`. Package versions stay per-node (no central package management).
5. **Supersede in part** the TFM / intentional-divergence wording in
   [ADR 0004](0004-azure-functions-series.md) and
   [ADR 0005](0005-azure-service-bus-series.md). Series structure, hosting
   (Flex), and teaching goals in those ADRs remain in force. Functions practice
   uses `--runtime-version 10.0` and .NET 10 SDK prerequisites.

## Consequences

- Uniform builds and clearer contributor prerequisites; one SDK band for the repo.
- Nodes are slightly less drop-in copy-pasteable alone (they inherit TFM from the
  repo root). That trade-off favors consistency over perfect isolation for TFM.
- Future readers of ADR 0004/0005 should treat net8 / “intentional divergence”
  lines as historical; this ADR is the current rule.
