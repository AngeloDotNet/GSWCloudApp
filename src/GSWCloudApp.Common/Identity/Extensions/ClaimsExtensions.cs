using System.Security.Claims;
using System.Security.Principal;

namespace GSWCloudApp.Common.Identity.Extensions;

public static class ClaimsExtensions
{
    /// <summary>
    /// Gets the user ID from the claims principal.
    /// </summary>
    /// <param name="user">The principal representing the user.</param>
    /// <returns>The user ID as a <see cref="Guid"/>.</returns>
    public static Guid GetId(this IPrincipal user) => Guid.Parse(user.GetClaimValue(ClaimTypes.NameIdentifier));

    /// <summary>
    /// Gets the value of a specific claim type from the claims principal.
    /// </summary>
    /// <param name="user">The principal representing the user.</param>
    /// <param name="claimType">The type of the claim to retrieve.</param>
    /// <returns>The value of the specified claim type.</returns>
    public static string GetClaimValue(this IPrincipal user, string claimType) => ((ClaimsPrincipal)user).FindFirst(claimType)?.Value!;
}
