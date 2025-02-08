namespace GSWCloudApp.Common.Identity.Options;

/// <summary>
/// Represents the security options for user authentication and authorization.
/// </summary>
public class SecurityOptions
{
    /// <summary>
    /// Gets a value indicating whether a digit is required in the password.
    /// </summary>
    public bool RequireDigit { get; init; } = true;

    /// <summary>
    /// Gets the required length of the password.
    /// </summary>
    public int RequireLenght { get; init; } = 8;

    /// <summary>
    /// Gets a value indicating whether an uppercase letter is required in the password.
    /// </summary>
    public bool RequireUppercase { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether a lowercase letter is required in the password.
    /// </summary>
    public bool RequireLowercase { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether a non-alphanumeric character is required in the password.
    /// </summary>
    public bool RequireNonAlphanumeric { get; init; } = true;

    /// <summary>
    /// Gets the number of unique characters required in the password.
    /// </summary>
    public int RequireUniqueChars { get; init; } = 4;

    /// <summary>
    /// Gets a value indicating whether a unique email is required for each user.
    /// </summary>
    public bool RequireUniqueEmail { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether email confirmation is required for user registration.
    /// </summary>
    public bool RequireConfirmedEmail { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether new users are allowed.
    /// </summary>
    public bool AllowedForNewUsers { get; init; } = true;

    /// <summary>
    /// Gets the maximum number of failed access attempts before a user is locked out.
    /// </summary>
    public int MaxFailedAccessAttempts { get; init; } = 5;

    /// <summary>
    /// Gets the default lockout time span after the maximum number of failed access attempts is reached.
    /// </summary>
    public TimeSpan DefaultLockoutTimeSpan { get; init; } = TimeSpan.FromMinutes(5);
}
