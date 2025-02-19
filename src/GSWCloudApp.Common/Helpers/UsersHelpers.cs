using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace GSWCloudApp.Common.Helpers;

//TODO: Add XML documentation
public static class UsersHelpers
{
    /// <summary>
    /// Retrieves the user ID from the current HTTP context.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor to retrieve the current HTTP context.</param>
    /// <returns>The user ID as a <see cref="Guid"/>. Returns <see cref="Guid.Empty"/> if the user ID is not found.</returns>
    public static Guid GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        var guid = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = guid != null ? Guid.Parse(guid) : Guid.Empty;

        return userId;
    }
}
