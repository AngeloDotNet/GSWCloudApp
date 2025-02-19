namespace GSWCloudApp.Common.Identity.Options;

/// <summary>
/// Represents the security options for user authentication and authorization.
/// </summary>
public class SecurityOptions
{
    /// <summary>
    /// Gets a value indicating whether a digit is required in the password.
    /// </summary>
    public bool RequiredDigit { get; init; } = true;

    /// <summary>
    /// Gets the required length of the password.
    /// </summary>
    public int RequiredLenght { get; init; } = 8;

    /// <summary>
    /// Gets a value indicating whether an uppercase letter is required in the password.
    /// </summary>
    public bool RequiredUppercase { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether a lowercase letter is required in the password.
    /// </summary>
    public bool RequiredLowercase { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether a non-alphanumeric character is required in the password.
    /// </summary>
    public bool RequiredNonAlphanumeric { get; init; } = true;

    /// <summary>
    /// Gets the number of unique characters required in the password.
    /// </summary>
    public int RequiredUniqueChars { get; init; } = 4;

    /// <summary>
    /// Gets a value indicating whether a unique email is required for each user.
    /// </summary>
    public bool RequiredUniqueEmail { get; init; } = true;

    /// <summary>
    /// Gets a value indicating whether email confirmation is required for user registration.
    /// </summary>
    public bool RequiredConfirmedEmail { get; init; } = true;

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
