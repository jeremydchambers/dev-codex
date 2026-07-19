# Azure Key Vault Secrets

## What it demonstrates

A basic .NET 10 console app that reads a **Secret** from **Azure Key Vault** using
`SecretClient` and `DefaultAzureCredential`. The sample checks that the value is
present without printing it.

## Key ideas

- Prefer identity (`DefaultAzureCredential`) over keys or client secrets.
- Configure the vault URI and Secret name with environment variables — not source code.
- Grant data-plane access with RBAC (**Key Vault Secrets User**), not access policies.
- Keep orchestration (validation + error messages) separate from the Azure SDK wiring so it stays testable.

## How to run

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
- An Azure subscription where you can create a Key Vault

### 1. Sign in

```bash
az login
```

### 2. Create a Key Vault (RBAC permission model)

```bash
az group create --name rg-dev-codex-kv --location eastus

az keyvault create \
  --name <your-unique-vault-name> \
  --resource-group rg-dev-codex-kv \
  --location eastus \
  --enable-rbac-authorization true
```

### 3. Grant yourself Key Vault Secrets User

```bash
VAULT_ID=$(az keyvault show --name <your-unique-vault-name> --resource-group rg-dev-codex-kv --query id -o tsv)
USER_ID=$(az ad signed-in-user show --query id -o tsv)

az role assignment create \
  --role "Key Vault Secrets User" \
  --assignee-object-id "$USER_ID" \
  --assignee-principal-type User \
  --scope "$VAULT_ID"
```

If you also need to *create* Secrets (not only read), assign **Key Vault Secrets Officer** for your user while setting up the demo.

### 4. Store a Secret

```bash
az keyvault secret set \
  --vault-name <your-unique-vault-name> \
  --name demo-secret \
  --value "replace-me"
```

### 5. Run the app

From `azure-key-vault--secrets/`:

```bash
export KEY_VAULT_URI="https://<your-unique-vault-name>.vault.azure.net/"
export SECRET_NAME="demo-secret"
dotnet run --project AzureKeyVaultSecrets
```

PowerShell:

```powershell
$env:KEY_VAULT_URI = "https://<your-unique-vault-name>.vault.azure.net/"
$env:SECRET_NAME = "demo-secret"
dotnet run --project AzureKeyVaultSecrets
```

On success you should see a message like:

```text
Retrieved Secret 'demo-secret' — value is present.
```

The Secret **value is never printed**.

### Tests

```bash
dotnet test
```

## Layout

| Path | Role |
|------|------|
| `AzureKeyVaultSecrets/Program.cs` | Wires `SecretClient` + `DefaultAzureCredential` |
| `AzureKeyVaultSecrets/SecretRead.cs` | Orchestrates validation, presence check, and error messages |
| `AzureKeyVaultSecrets.Tests/` | Unit tests at the `SecretRead` seam (no live Key Vault) |

## References

- [Azure.Security.KeyVault.Secrets README](https://learn.microsoft.com/dotnet/api/overview/azure/security.keyvault.secrets-readme)
- [DefaultAzureCredential](https://learn.microsoft.com/dotnet/api/azure.identity.defaultazurecredential)
- [Azure Key Vault RBAC](https://learn.microsoft.com/azure/key-vault/general/rbac-guide)
