﻿using System.Security.Claims;
using System.Security.Principal;

namespace AutenticazioneSvc.BusinessLayer.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetId(this IPrincipal user)
    {
        var value = GetClaimValue(user, ClaimTypes.NameIdentifier);
        return Guid.Parse(value);
    }

    public static string GetClaimValue(this IPrincipal user, string claimType)
    {
        var value = ((ClaimsPrincipal)user).FindFirst(claimType)?.Value;

        return value!;
    }
}