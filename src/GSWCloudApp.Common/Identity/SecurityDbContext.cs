using GSWCloudApp.Common.Identity.Entities.Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GSWCloudApp.Common.Identity;

/// <summary>
/// Represents the database context for security-related entities, including users, roles, and their claims.
/// </summary>
/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
public class SecurityDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
{ }
