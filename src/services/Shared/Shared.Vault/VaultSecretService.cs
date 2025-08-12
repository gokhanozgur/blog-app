using VaultSharp;

namespace Shared.Vault;

public class VaultSecretService : IVaultSecretService
{
    private readonly IVaultClient _vaultClient;

    public VaultSecretService(IVaultClient vaultClient)
    {
        _vaultClient = vaultClient;
    }

    public async Task<string> GetCubbyholeFieldAsync(string path, string fieldName)
    {
        var secret = await _vaultClient.V1.Secrets.Cubbyhole.ReadSecretAsync(path);
        var data = secret.Data; // IDictionary<string, object>
        if (!data.TryGetValue(fieldName, out var value) || value is null)
            throw new KeyNotFoundException($"Field '{fieldName}' not found in cubbyhole path '{path}'.");
        return value.ToString()!;
    }

    public string GetCubbyholeField(string path, string fieldName)
    {
        var secret = _vaultClient.V1.Secrets.Cubbyhole
            .ReadSecretAsync(path)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var data = secret.Data;
        if (!data.TryGetValue(fieldName, out var value) || value is null)
            throw new KeyNotFoundException($"Field '{fieldName}' not found in cubbyhole path '{path}'.");
        return value.ToString()!;
    }
}