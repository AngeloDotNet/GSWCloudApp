using Microsoft.AspNetCore.Identity;

namespace GSWCloudApp.Common.Identity.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    { }

    public ApplicationRole(string roleName) : base(roleName)
    { }

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}