# dev-codex

Glossary for this repo's product language — demo **nodes** and the domains they teach. Not implementation notes.

## Language

**Node**:
A self-contained top-level folder that demonstrates one concept, with its own `README.md`.
_Avoid_: Project, sample, package (when meaning a demo folder)

**Series**:
A named group of Nodes (possibly one). Nodes in a Series share a folder-name left segment before `--`. An ordered learning path with README Series/Prev/Next navigation is optional.
_Avoid_: Tutorial, track, course (when meaning this node group); Catalog Prefix, Topic Family (when meaning Series)

**Secret**:
A named string value stored in and retrieved from Azure Key Vault (not a Key Vault cryptographic key or certificate).
_Avoid_: Env var, app setting, configuration value (when meaning the Key Vault resource)

**Key Vault**:
Azure's managed vault used here specifically as the store from which the node reads Secrets.
_Avoid_: App Configuration, Parameter Store (as synonyms for this store)

**Standard target framework**:
The single .NET target framework every runnable .NET Node in this repository must use.
_Avoid_: Per-node framework, mixed TFMs (when meaning an intentional split)
