using Microsoft.AspNetCore.Identity;

namespace GSWCloudApp.Common.Identity.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual ApplicationRole Role { get; set; } = null!;
}