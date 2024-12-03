using System.Security.Claims;
using AutenticazioneSvc.BusinessLayer.Extensions;
using AutenticazioneSvc.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AutenticazioneSvc.BusinessLayer.Requirements;

public class UserActiveHandler(UserManager<ApplicationUser> userManager) : AuthorizationHandler<UserActiveRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserActiveRequirement requirement)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var userId = context.User.GetId();
            var user = await userManager.FindByIdAsync(userId.ToString());
            var securityStamp = context.User.GetClaimValue(ClaimTypes.SerialNumber);

            if (user != null && user.LockoutEnd.GetValueOrDefault() <= DateTimeOffset.UtcNow && securityStamp == user.SecurityStamp)
            {
                context.Succeed(requirement);
            }
        }
    }
}