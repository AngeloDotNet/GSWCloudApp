﻿using GSWCloudApp.Common.Vault.Options;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace GSWCloudApp.Common.Vault.Provider;

/// <summary>
/// Provides methods to interact with Vault secrets.
/// </summary>
public class VaultSecretProvider
{
    private readonly IVaultClient vaultClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="VaultSecretProvider"/> class.
    /// </summary>
    /// <param name="vaultUri">The URI of the Vault server.</param>
    /// <param name="vaultToken">The token used for authentication with the Vault server.</param>
    public VaultSecretProvider(string vaultUri, string vaultToken)
    {
        var authMethod = new TokenAuthMethodInfo(vaultToken);
        var vaultClientSettings = new VaultClientSettings(vaultUri, authMethod);

        vaultClient = new VaultClient(vaultClientSettings);
    }

    /// <summary>
    /// Gets the value of a secret from Vault.
    /// </summary>
    /// <param name="vaultSettings">The Vault settings.</param>
    /// <param name="path">The path of the secret.</param>
    /// <param name="key">The key of the secret.</param>
    /// <returns>The value of the secret.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the key is not found in Vault.</exception>
    public async Task<string> GetSecretValueAsync(VaultOptions vaultSettings, string path, string key)
    {
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: path, mountPoint: vaultSettings.MountPoint);

        if (secret.Data.Data.TryGetValue(key, out var value))
        {
            return value.ToString()!;
        }

        throw new KeyNotFoundException($"Key '{key}' not found in Vault at path '{path}'");
    }

    /// <summary>
    /// Sets the value of a secret in Vault.
    /// </summary>
    /// <param name="path">The path of the secret.</param>
    /// <param name="key">The key of the secret.</param>
    /// <param name="value">The value of the secret.</param>
    /// <param name="mountPoint">The mount point of the Vault secrets engine.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
    public async Task<bool> SetSecretValueAsync(string path, string key, string value, string mountPoint)
    {
        var secret = new Dictionary<string, object>
        {
            { key, value }
        };

        var writeSecret = await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(path: path, secret, checkAndSet: null, mountPoint: mountPoint);

        return true;
    }
}
