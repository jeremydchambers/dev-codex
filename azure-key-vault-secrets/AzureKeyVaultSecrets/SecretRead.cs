namespace AzureKeyVaultSecrets;

public sealed record SecretReadResult(bool Succeeded, string Message, int ExitCode);

public static class SecretRead
{
    public static async Task<SecretReadResult> ExecuteAsync(
        string? vaultUri,
        string? secretName,
        Func<Uri, string, CancellationToken, Task<GetSecretResult>> getSecretAsync,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(vaultUri))
        {
            return new SecretReadResult(
                Succeeded: false,
                Message: "Missing KEY_VAULT_URI. Set it to your Key Vault URI (for example https://my-vault.vault.azure.net/).",
                ExitCode: 1);
        }

        if (string.IsNullOrWhiteSpace(secretName))
        {
            return new SecretReadResult(
                Succeeded: false,
                Message: "Missing SECRET_NAME. Set it to the name of the Secret to read.",
                ExitCode: 1);
        }

        if (!Uri.TryCreate(vaultUri, UriKind.Absolute, out var vault))
        {
            return new SecretReadResult(
                Succeeded: false,
                Message: "KEY_VAULT_URI is not a valid absolute URI.",
                ExitCode: 1);
        }

        var getResult = await getSecretAsync(vault, secretName, cancellationToken).ConfigureAwait(false);

        return getResult switch
        {
            GetSecretFound found when string.IsNullOrWhiteSpace(found.Value) =>
                new SecretReadResult(
                    Succeeded: false,
                    Message: $"Secret '{secretName}' was retrieved but its value is empty or whitespace.",
                    ExitCode: 1),
            GetSecretFound =>
                new SecretReadResult(
                    Succeeded: true,
                    Message: $"Retrieved Secret '{secretName}' — value is present.",
                    ExitCode: 0),
            GetSecretUnauthorized =>
                new SecretReadResult(
                    Succeeded: false,
                    Message: "Authentication or authorization failed. Run `az login`, and ensure this identity has the Key Vault Secrets User role on the vault.",
                    ExitCode: 1),
            GetSecretNotFound =>
                new SecretReadResult(
                    Succeeded: false,
                    Message: $"Secret '{secretName}' was not found in the Key Vault.",
                    ExitCode: 1),
            GetSecretFailed failed =>
                new SecretReadResult(
                    Succeeded: false,
                    Message: $"Failed to read Secret '{secretName}': {failed.Detail}",
                    ExitCode: 1),
            _ =>
                new SecretReadResult(
                    Succeeded: false,
                    Message: $"Failed to read Secret '{secretName}': unexpected result.",
                    ExitCode: 1),
        };
    }
}

public abstract record GetSecretResult;

public sealed record GetSecretFound(string Value) : GetSecretResult;

public sealed record GetSecretUnauthorized : GetSecretResult;

public sealed record GetSecretNotFound : GetSecretResult;

public sealed record GetSecretFailed(string Detail) : GetSecretResult;
