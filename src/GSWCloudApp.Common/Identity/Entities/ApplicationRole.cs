using Microsoft.AspNetCore.Identity;

namespace GSWCloudApp.Common.Identity.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
    /// </summary>
    public ApplicationRole()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationRole"/> class with a specified role name.
    /// </summary>
    /// <param name="roleName">The name of the role.</param>
    public ApplicationRole(string roleName) : base(roleName)
    { }

    /// <summary>
    /// Gets or sets the collection of user roles associated with this role.
    /// </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}
