namespace GSWCloudApp.Common.Identity.Options;

public class SecurityOptions
{
    public bool RequireDigit { get; init; } = true;
    public int RequireLenght { get; init; } = 8;
    public bool RequireUppercase { get; init; } = true;
    public bool RequireLowercase { get; init; } = true;
    public bool RequireNonAlphanumeric { get; init; } = true;
    public int RequireUniqueChars { get; init; } = 4;

    public bool RequireUniqueEmail { get; init; } = true;
    public bool RequireConfirmedEmail { get; init; } = true;

    public bool AllowedForNewUsers { get; init; } = true;
    public int MaxFailedAccessAttempts { get; init; } = 5;
    public TimeSpan DefaultLockoutTimeSpan { get; init; } = TimeSpan.FromMinutes(5);
}
