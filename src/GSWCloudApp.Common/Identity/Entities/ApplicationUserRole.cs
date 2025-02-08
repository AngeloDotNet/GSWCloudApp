using Microsoft.AspNetCore.Identity;

namespace GSWCloudApp.Common.Identity.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    /// <summary>
    /// Gets or sets the user associated with this user role.
    /// </summary>
    public virtual ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role associated with this user role.
    /// </summary>
    public virtual ApplicationRole Role { get; set; } = null!;
}
