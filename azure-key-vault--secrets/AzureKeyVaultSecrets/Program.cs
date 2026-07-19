using Azure;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureKeyVaultSecrets;

// Auth: DefaultAzureCredential (az login locally; managed identity in Azure).
// Sample pattern: https://learn.microsoft.com/dotnet/api/overview/azure/security.keyvault.secrets-readme
var result = await SecretRead.ExecuteAsync(
    Environment.GetEnvironmentVariable("KEY_VAULT_URI"),
    Environment.GetEnvironmentVariable("SECRET_NAME"),
    GetSecretAsync);

Console.WriteLine(result.Message);
return result.ExitCode;

static async Task<GetSecretResult> GetSecretAsync(
    Uri vaultUri,
    string secretName,
    CancellationToken cancellationToken)
{
    try
    {
        var client = new SecretClient(vaultUri, new DefaultAzureCredential());
        KeyVaultSecret secret = await client
            .GetSecretAsync(secretName, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        return new GetSecretFound(secret.Value);
    }
    catch (AuthenticationFailedException)
    {
        return new GetSecretUnauthorized();
    }
    catch (RequestFailedException ex) when (ex.Status is 401 or 403)
    {
        return new GetSecretUnauthorized();
    }
    catch (RequestFailedException ex) when (ex.Status == 404)
    {
        return new GetSecretNotFound();
    }
    catch (RequestFailedException ex)
    {
        return new GetSecretFailed(ex.Message);
    }
}
