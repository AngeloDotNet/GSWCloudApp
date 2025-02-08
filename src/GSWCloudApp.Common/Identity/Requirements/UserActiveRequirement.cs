using Microsoft.AspNetCore.Authorization;

namespace GSWCloudApp.Common.Identity.Requirements;

/// <summary>
/// Represents a requirement for a user to be active.
/// </summary>
public class UserActiveRequirement : IAuthorizationRequirement
{ }
