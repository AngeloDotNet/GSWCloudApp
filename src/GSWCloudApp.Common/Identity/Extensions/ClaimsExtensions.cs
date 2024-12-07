using System.Security.Claims;
using System.Security.Principal;

namespace GSWCloudApp.Common.Identity.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetId(this IPrincipal user)
    {
        var value = user.GetClaimValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(value);
    }

    public static string GetClaimValue(this IPrincipal user, string claimType)
    {
        var value = ((ClaimsPrincipal)user).FindFirst(claimType)?.Value;

        return value!;
    }
}