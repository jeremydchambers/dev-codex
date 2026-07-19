using AzureKeyVaultSecrets;

namespace AzureKeyVaultSecrets.Tests;

public class SecretReadTests
{
    [Fact]
    public async Task Missing_vault_uri_fails_with_clear_message()
    {
        var result = await SecretRead.ExecuteAsync(
            vaultUri: null,
            secretName: "demo-secret",
            getSecretAsync: (_, _, _) => throw new InvalidOperationException("should not call Key Vault"));

        Assert.False(result.Succeeded);
        Assert.Contains("KEY_VAULT_URI", result.Message, StringComparison.Ordinal);
        Assert.Equal(1, result.ExitCode);
    }

    [Fact]
    public async Task Missing_secret_name_fails_with_clear_message()
    {
        var result = await SecretRead.ExecuteAsync(
            vaultUri: "https://my-vault.vault.azure.net/",
            secretName: "  ",
            getSecretAsync: (_, _, _) => throw new InvalidOperationException("should not call Key Vault"));

        Assert.False(result.Succeeded);
        Assert.Contains("SECRET_NAME", result.Message, StringComparison.Ordinal);
        Assert.Equal(1, result.ExitCode);
    }

    [Fact]
    public async Task Present_secret_value_succeeds_without_printing_value()
    {
        var result = await SecretRead.ExecuteAsync(
            vaultUri: "https://my-vault.vault.azure.net/",
            secretName: "demo-secret",
            getSecretAsync: (_, _, _) => Task.FromResult<GetSecretResult>(new GetSecretFound("s3cret-value")));

        Assert.True(result.Succeeded);
        Assert.Contains("demo-secret", result.Message, StringComparison.Ordinal);
        Assert.DoesNotContain("s3cret-value", result.Message, StringComparison.Ordinal);
        Assert.Equal(0, result.ExitCode);
    }

    [Fact]
    public async Task Empty_or_whitespace_secret_value_fails()
    {
        var result = await SecretRead.ExecuteAsync(
            vaultUri: "https://my-vault.vault.azure.net/",
            secretName: "demo-secret",
            getSecretAsync: (_, _, _) => Task.FromResult<GetSecretResult>(new GetSecretFound("   ")));

        Assert.False(result.Succeeded);
        Assert.Contains("empty or whitespace", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(1, result.ExitCode);
    }

    [Fact]
    public async Task Unauthorized_maps_to_clear_auth_message()
    {
        var result = await SecretRead.ExecuteAsync(
            vaultUri: "https://my-vault.vault.azure.net/",
            secretName: "demo-secret",
            getSecretAsync: (_, _, _) => Task.FromResult<GetSecretResult>(new GetSecretUnauthorized()));

        Assert.False(result.Succeeded);
        Assert.Contains("Authentication or authorization failed", result.Message, StringComparison.Ordinal);
        Assert.Contains("Key Vault Secrets User", result.Message, StringComparison.Ordinal);
        Assert.Equal(1, result.ExitCode);
    }

    [Fact]
    public async Task Not_found_maps_to_clear_message()
    {
        var result = await SecretRead.ExecuteAsync(
            vaultUri: "https://my-vault.vault.azure.net/",
            secretName: "missing-secret",
            getSecretAsync: (_, _, _) => Task.FromResult<GetSecretResult>(new GetSecretNotFound()));

        Assert.False(result.Succeeded);
        Assert.Contains("missing-secret", result.Message, StringComparison.Ordinal);
        Assert.Contains("not found", result.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(1, result.ExitCode);
    }

    [Fact]
    public async Task Invalid_vault_uri_fails_without_calling_key_vault()
    {
        var called = false;
        var result = await SecretRead.ExecuteAsync(
            vaultUri: "not-a-uri",
            secretName: "demo-secret",
            getSecretAsync: (_, _, _) =>
            {
                called = true;
                return Task.FromResult<GetSecretResult>(new GetSecretFound("x"));
            });

        Assert.False(called);
        Assert.False(result.Succeeded);
        Assert.Contains("KEY_VAULT_URI", result.Message, StringComparison.Ordinal);
        Assert.Equal(1, result.ExitCode);
    }
}
