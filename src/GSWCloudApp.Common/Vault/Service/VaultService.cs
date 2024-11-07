using GSWCloudApp.Common.Vault.Options;
using GSWCloudApp.Common.Vault.Provider;

namespace GSWCloudApp.Common.Vault.Service;

public static class VaultService
{
    public static async Task<bool> WriteVaultSecretAsync(VaultOptions vaultSettings, string path, string key, string value, string mountPoint)
    {
        var vaultSecretsProvider = new VaultSecretProvider(vaultSettings!.Address, vaultSettings.Token);

        var isCreated = await vaultSecretsProvider.SetSecretValueAsync(path: path, key: key, value: value, mountPoint: mountPoint);

        return isCreated;
    }

    public static async Task<string> ReadVaultSecretAsync(VaultOptions vaultSettings, string path, string key)
    {
        var vaultSecretsProvider = new VaultSecretProvider(vaultSettings!.Address, vaultSettings.Token);

        return await vaultSecretsProvider.GetSecretValueAsync(vaultSettings, path: path, key: key);
    }
}