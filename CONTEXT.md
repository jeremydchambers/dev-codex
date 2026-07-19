# dev-codex

Glossary for this repo's product language — demo **nodes** and the domains they teach. Not implementation notes.

## Language

**Node**:
A self-contained top-level folder that demonstrates one concept, with its own `README.md`.
_Avoid_: Project, sample, package (when meaning a demo folder)

**Series**:
An ordered set of Nodes that share a naming prefix and README Series/Prev/Next navigation.
_Avoid_: Tutorial, track, course (when meaning this ordered node path)

**Secret**:
A named string value stored in and retrieved from Azure Key Vault (not a Key Vault cryptographic key or certificate).
_Avoid_: Env var, app setting, configuration value (when meaning the Key Vault resource)

**Key Vault**:
Azure's managed vault used here specifically as the store from which the node reads Secrets.
_Avoid_: App Configuration, Parameter Store (as synonyms for this store)
