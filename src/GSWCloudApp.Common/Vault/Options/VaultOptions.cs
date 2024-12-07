namespace GSWCloudApp.Common.Vault.Options;

/// <summary>
/// Options for configuring the Vault client.
/// </summary>
public class VaultOptions
{
    /// <summary>
    /// Gets or sets the address of the Vault server.
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Gets or sets the token used for authentication with the Vault server.
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Gets or sets the mount point for the Vault secrets engine.
    /// </summary>
    public string MountPoint { get; set; } = null!;
}
