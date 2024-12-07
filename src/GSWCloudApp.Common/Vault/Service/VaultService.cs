using GSWCloudApp.Common.Vault.Options;
using GSWCloudApp.Common.Vault.Provider;

namespace GSWCloudApp.Common.Vault.Service;

/// <summary>  
/// Provides methods to interact with Vault secrets.  
/// </summary>  
public static class VaultService
{
    /// <summary>  
    /// Writes a secret to Vault.  
    /// </summary>  
    /// <param name="vaultSettings">The Vault settings.</param>  
    /// <param name="path">The path of the secret.</param>  
    /// <param name="key">The key of the secret.</param>  
    /// <param name="value">The value of the secret.</param>  
    /// <param name="mountPoint">The mount point of the Vault secrets engine.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>  
    public static async Task<bool> WriteVaultSecretAsync(VaultOptions vaultSettings, string path, string key, string value, string mountPoint)
    {
        var vaultSecretsProvider = new VaultSecretProvider(vaultSettings!.Address, vaultSettings.Token);

        return await vaultSecretsProvider.SetSecretValueAsync(path: path, key: key, value: value, mountPoint: mountPoint);
    }

    /// <summary>  
    /// Reads a secret from Vault.  
    /// </summary>  
    /// <param name="vaultSettings">The Vault settings.</param>  
    /// <param name="path">The path of the secret.</param>  
    /// <param name="key">The key of the secret.</param>  
    /// <returns>A task that represents the asynchronous operation. The task result contains the value of the secret.</returns>  
    public static async Task<string> ReadVaultSecretAsync(VaultOptions vaultSettings, string path, string key)
    {
        var vaultSecretsProvider = new VaultSecretProvider(vaultSettings!.Address, vaultSettings.Token);

        return await vaultSecretsProvider.GetSecretValueAsync(vaultSettings, path: path, key: key);
    }
}
