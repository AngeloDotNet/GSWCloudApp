﻿using System.Security.Claims;
using GSWCloudApp.Common.Identity.Entities;
using GSWCloudApp.Common.Identity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GSWCloudApp.Common.Identity.Requirements;

public class UserActiveHandler(UserManager<ApplicationUser> userManager) : AuthorizationHandler<UserActiveRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserActiveRequirement requirement)
    {
        if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
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