using Microsoft.AspNetCore.Identity;

namespace GSWCloudApp.Common.Identity.Entities.Application;

public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the refresh token for the user.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of the refresh token.
    /// </summary>
    public DateTime? RefreshTokenExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets the collection of user roles associated with this user.
    /// </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
}
