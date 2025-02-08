namespace GSWCloudApp.Common.Identity.Options;

public class JwtOptions
{
    /// <summary>
    /// Gets the issuer of the JWT.
    /// </summary>
    public string Issuer { get; init; } = null!;

    /// <summary>
    /// Gets the audience of the JWT.
    /// </summary>
    public string Audience { get; init; } = null!;

    /// <summary>
    /// Gets the security key used to sign the JWT.
    /// </summary>
    public string SecurityKey { get; init; } = null!;

    /// <summary>
    /// Gets the expiration time in minutes for the access token.
    /// </summary>
    public int AccessTokenExpirationMinutes { get; init; }

    /// <summary>
    /// Gets the expiration time in minutes for the refresh token.
    /// </summary>
    public int RefreshTokenExpirationMinutes { get; init; }
}
